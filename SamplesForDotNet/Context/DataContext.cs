using DotNet8NewFeatures.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DotNet8NewFeatures.Context
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<TreeNode> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TreeNode>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Parent)
                    .WithMany(x => x.SubCategories)
                    .HasForeignKey(x => x.ParentId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasData(
                [
                    new(1, null, "Root", HierarchyId.GetRoot()),
                    new(2, 1, "Technology", HierarchyId.Parse("/1/")),
                    new(3, 1, "HR", HierarchyId.Parse("/2/")),
                    new(4, 2, "Domestic-Flight", HierarchyId.Parse("/1/2/")),
                ]);
            });
        }
    }
}
