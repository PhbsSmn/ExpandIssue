using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpandIssue.Models.EntityFramework;
using ExpandIssue.OData;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ExpandIssue
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;


        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dbConnectionString = Configuration["DbConnectionString"];
            services.AddDbContext<ExpandIssueDbContext>(options =>
            {
                options.UseSqlServer(dbConnectionString);
#if DEBUG
                options.EnableSensitiveDataLogging(true);
#endif
            });

            // Odata support
            services.AddOData();
            services.AddTransient<ExpandIssueModelBuilder>();

            // Add controllers
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ExpandIssueModelBuilder modelBuilder, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // Default odata config, select will otherwise do difficult if only asking for specific properties. Not sure for the other options (count, filter, orderby, maxtop).
                endpoints.Count().Filter().Select().MaxTop(null).Expand();
                endpoints.MapODataRoute("ODataRoutes", "odata", modelBuilder.GetEdmModel(serviceProvider));
            });
        }
    }
}
