using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using WebApp.DomainModel;

namespace WebApp.MysqlDataService
{
    public class NorthwindContect : DbContext
    {
        public NorthwindContect() : base("northwind") {}

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("category");
            modelBuilder.Entity<Category>().Property(c => c.Id).HasColumnName("categoryid");
            modelBuilder.Entity<Category>().Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Category>().Property(c => c.Name).HasColumnName("categoryname");

            base.OnModelCreating(modelBuilder);
        }
    }
}