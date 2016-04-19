using System.Collections.Generic;
using WebApp.DomainModel;

namespace WebApp.Services
{
    public interface ICategoryDataService
    {
        List<Category> GetAll();
        Category GetById(int id);
        void Insert(Category category);
        bool Update(Category category);
        bool Delete(int id);
    }
}