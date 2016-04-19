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
            // if you dont do this mysql entity framwork implementation will assume that 
            // you're using auto increment id, and since this is not the case all new categories
            // will get the id = 0 - i.e. you get primary key constaint violation on your second
            // insert. But this disable auto increment and assume you will provide an id
            modelBuilder.Entity<Category>().Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Category>().Property(c => c.Name).HasColumnName("categoryname");

            base.OnModelCreating(modelBuilder);
        }
    }
}