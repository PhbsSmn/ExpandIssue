using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExpandIssue.Models.EntityFramework
{
    public partial class ExpandIssueDbContext : DbContext
    {
        public ExpandIssueDbContext()
        {
        }

        public ExpandIssueDbContext(DbContextOptions<ExpandIssueDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChildTable> ChildTable { get; set; }
        public virtual DbSet<MasterTable> MasterTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChildTable>(entity =>
            {
                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.HasOne(d => d.MasterTable)
                    .WithMany(p => p.ChildTable)
                    .HasForeignKey(d => d.MasterTableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChildTable_MasterTable");
            });

            modelBuilder.Entity<MasterTable>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
