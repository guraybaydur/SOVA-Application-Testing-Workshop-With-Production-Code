using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using Moq;
using NUnit.Framework;
using WebApp.Controllers;
using WebApp.DomainModel;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Tests.WebApp.Unit
{
    public class CategoriesControllerTests
    {
        public Mock<ICategoryDataService> ServiceMock;
        public Mock<UrlHelper> UrlHelperMock;

        [SetUp]
        public void SetUp()
        {
            ServiceMock = new Mock<ICategoryDataService>();
            UrlHelperMock = new Mock<UrlHelper>();
        }

        [Test]
        public void Get_WithoutArguments_ReturnOk()
        {
            //Arrange
            var controller = new CategoriesController(ServiceMock.Object);

            //Act
            var response = controller.Get() as OkNegotiatedContentResult<List<Category>>;

            //Assert
            Assert.NotNull(response);
        }

        [Test]
        public void Get_WithValidId_ReturnOkWithCategoryModel()
        {
            //Arrange
            ServiceMock.Setup(m => m.GetById(It.IsAny<int>()))
                .Returns(new Category {Name = "Beverages"});

            var controller = new CategoriesController(ServiceMock.Object);
            controller.Url = UrlHelperMock.Object;

            //Act
            var response = controller.Get(1) as OkNegotiatedContentResult<CategoryModel>;

            //Assert
            Assert.NotNull(response);
        }

        [Test]
        public void Get_WithValidId_MakeCallToGetById()
        {
            //Arrange
            ServiceMock.Setup(m => m.GetById(It.IsAny<int>()))
                .Returns(new Category {Name = "Beverages"});

            var controller = new CategoriesController(ServiceMock.Object);
            controller.Url = UrlHelperMock.Object;

            //Act
            controller.Get(1);

            //Assert
            ServiceMock.Verify(m => m.GetById(It.Is<int>(x => x == 1)), Times.Once);
        }


        [Test]
        public void Get_WithInvalidId_ReturnNotfound()
        {
            //Arrange
            var controller = new CategoriesController(ServiceMock.Object);

            //Act
            var response = controller.Get(-1) as NotFoundResult;

            //Assert
            Assert.NotNull(response);
        }

        [Test]
        public void Get_WithInvalidId_MakeCallToGetById()
        {
            //Arrange
            var controller = new CategoriesController(ServiceMock.Object);

            //Act
            controller.Get(-1);

            //Assert
            ServiceMock.Verify(m => m.GetById(It.Is<int>(x => x == -1)), Times.Once);
        }

        [Test]
        public void Post_WithCategoryModel_ReturnCreatedWithCategoryRoute()
        {
            //Arrange
            var controller = new CategoriesController(ServiceMock.Object);
            controller.Url = UrlHelperMock.Object;
            var categoryModel = new CategoryModel {Name = "Testing"};

            //Act
            var response =
                controller.Post(categoryModel) as CreatedNegotiatedContentResult<CategoryModel>;

            //Assert
            Assert.NotNull(response);
            Assert.AreEqual("CategoryRoute", response.Location.ToString());
        }

        [Test]
        public void Post_WithCategoryModel_CallInsert()
        {
            //Arrange
            ServiceMock.Setup(m => m.Insert(It.IsAny<Category>())).Callback<Category>(c => c.Id = 1);

            var controller = new CategoriesController(ServiceMock.Object)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage {RequestUri = new Uri("http://localhost/api/categories")}
            };
            WebApiConfig.Register(controller.Configuration);
            
            var categoryModel = new CategoryModel {Name = "Testing"};

            //Act
            var response = controller.Post(categoryModel) as CreatedNegotiatedContentResult<CategoryModel>;

            //Assert
            Assert.AreEqual("http://localhost/api/categories/1", response.Content.Url);
            ServiceMock.Verify(m => m.Insert(It.Is<Category>(c => c.Name == "Testing" && c.Id == 1)));
        }

        [Test]
        public void Delete_WithValidId_ReturnOk()
        {
            //Arrange
            ServiceMock.Setup(m => m.Delete(It.IsAny<int>())).Returns(true);
            var controller = new CategoriesController(ServiceMock.Object);

            //Act
            var response = controller.Delete(1) as OkResult;

            //Assert
            Assert.NotNull(response);
        }

        [Test]
        public void Delete_WithValidId_CallServiceDelete()
        {
            //Arrange
            var controller = new CategoriesController(ServiceMock.Object);

            //Act
            controller.Delete(1);

            //Assert
            ServiceMock.Verify(m => m.Delete(It.Is<int>(id => id == 1)));
        }

        [Test]
        public void Delete_WithInvalidId_ReturnNotFound()
        {
            //Arrange
            ServiceMock.Setup(m => m.Delete(It.IsAny<int>())).Returns(false);
            var controller = new CategoriesController(ServiceMock.Object);

            //Act
            var response = controller.Delete(-1) as NotFoundResult;

            //Assert
            Assert.NotNull(response);
            ServiceMock.Verify(m => m.Delete(It.Is<int>(id => id == -1)));
        }

        [Test]
        public void Put_WithValidIdMandCategoryModel_ReturnOk()
        {
            //Arrange
            ServiceMock.Setup(m => m.Update(It.IsAny<Category>())).Returns(true);
            var controller = new CategoriesController(ServiceMock.Object);
            var categoryModel = new CategoryModel {Name = "Testing"};

            //Act
            var response = controller.Put(1, categoryModel) as OkResult;

            //Assert
            Assert.NotNull(response);
        }

        [Test]
        public void Put_WithValidIdMandCategoryModel_CallServiceUpdate()
        {
            //Arrange
            var controller = new CategoriesController(ServiceMock.Object);
            var categoryModel = new CategoryModel { Name = "Testing" };

            //Act
            controller.Put(1, categoryModel);

            //Assert
            ServiceMock.Verify(m => m.Update(It.Is<Category>(c => c.Id == 1)));
        }

        [Test]
        public void Put_WithInvalidId_ReturnNotFound()
        {
            //Arrange
            ServiceMock.Setup(m => m.Update(It.IsAny<Category>())).Returns(false);
            var controller = new CategoriesController(ServiceMock.Object);
            var categoryModel = new CategoryModel { Name = "Testing" };

            //Act
            var response = controller.Put(-1, categoryModel) as NotFoundResult;

            //Assert
            Assert.NotNull(response);
        }
    }
}