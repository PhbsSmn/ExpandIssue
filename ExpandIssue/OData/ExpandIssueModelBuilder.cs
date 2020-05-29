using System;
using ExpandIssue.Models.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace ExpandIssue.OData
{
    public class ExpandIssueModelBuilder
    {
        #region Methods
        public IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            const int defaultPageSize = 100;

            var builder = new ODataConventionModelBuilder(serviceProvider)
            {
                Namespace = "ExpandIssue"
            };

            var masterTable = builder.EntitySet<MasterTableDetail>("MasterTables");
            masterTable.EntityType.Page(null, defaultPageSize);
            masterTable.HasManyBinding(m => m.Childs, "ChildTableInfo");

            return builder.GetEdmModel();
        }
        #endregion
    }
}