using System.Linq;
using NUnit.Framework;
using WebApp.DomainModel;
using WebApp.MysqlDataService;

namespace WebApp.Tests.MysqlDataService.Integration
{
    public class CategoryServiceTests
    {
        [Test]
        public void GetAll()
        {
            //Arrange
            var service = new MySqlCategoryService();

            //Act
            var result = service.GetAll();

            //Assert
            Assert.AreEqual(8, result.Count());
            Assert.AreEqual("Beverages", result.First().Name);
        }

        [Test]
        public void GetById_ValidId_Category()
        {
            //Arrange
            var service = new MySqlCategoryService();

            //Act
            var result = service.GetById(1);

            //Assert
            Assert.AreEqual("Beverages", result.Name);
        }

        [Test]
        public void Insert_Category()
        {
            //Arrange
            var service = new MySqlCategoryService();
            var category = new Category { Name = "Henrik"};
            //Act
            service.Insert(category);

            //Assert
            Assert.AreEqual("Henrik", service.GetById(category.Id).Name);

            //CleanUp
            service.Delete(category.Id);
        }

        [Test]
        public void Update_Category()
        {
            //Arrange
            var service = new MySqlCategoryService();
            var category = new Category { Name = "Henrik" };
            service.Insert(category);
            //Act
            category.Name = "Bulskov";
            service.Update(category);

            //Assert
            Assert.AreEqual("Bulskov", service.GetById(category.Id).Name);

            //CleanUp
            service.Delete(category.Id);
        }

        [Test]
        public void Delete_Id_RemoveCategory()
        {
            //Arrange
            var service = new MySqlCategoryService();
            var category = new Category { Name = "Henrik" };
            service.Insert(category);

            //Act
            service.Delete(category.Id);

            //Assert
            Assert.Null(service.GetById(category.Id));
        }
    }
}