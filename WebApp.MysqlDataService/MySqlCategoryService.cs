using System.Collections.Generic;
using System.Linq;
using WebApp.DomainModel;
using WebApp.Services;

namespace WebApp.MysqlDataService
{
    public class MySqlCategoryService : ICategoryDataService
    {
        public List<Category> GetAll()
        {
            using (var db = new NorthwindContect())
            {
                return db.Categories.ToList();
            }
        }

        public Category GetById(int id)
        {
            using (var db = new NorthwindContect())
            {
                return db.Categories.FirstOrDefault(x => x.Id == id);
            }
        }

        public void Insert(Category category)
        {
            using (var db = new NorthwindContect())
            {
                category.Id = db.Categories.Max(c => c.Id) + 1;
                db.Categories.Add(category);
                db.SaveChanges();
            }
        }

        public bool Update(Category category)
        {
            using (var db = new NorthwindContect())
            {
                var dbCategory = db.Categories.FirstOrDefault(c => c.Id == category.Id);
                if (dbCategory == null) return false;

                dbCategory.Name = category.Name;
                db.SaveChanges();
                return true;
            }
        }

        public bool Delete(int id)
        {
            using (var db = new NorthwindContect())
            {
                var category = db.Categories.FirstOrDefault(c => c.Id == id);
                if (category == null) return false;

                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
        }
    }
}