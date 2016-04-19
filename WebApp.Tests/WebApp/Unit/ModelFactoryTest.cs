using System.Web.Http.Routing;
using Moq;
using NUnit.Framework;
using WebApp.DomainModel;
using WebApp.Models;

namespace WebApp.Tests.WebApp.Unit
{
    public class ModelFactoryTest
    {
        [Test]
        public void Map_Category_CategoryModel()
        {
            //Arrange
            var urlHelperMock = new Mock<UrlHelper>();
            urlHelperMock.Setup(m => m.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("Link");

            var category = new Category {Id = 1, Name = "TestingMapping"};

            //Act
            var model = ModelFactory.Map(category, urlHelperMock.Object);

            //Assert
            Assert.AreEqual("Link", model.Url, "Url");
            Assert.AreEqual("TestingMapping", model.Name, "Name");
            urlHelperMock.Verify(m => m.Link(
                It.Is<string>(x => x == "CategoryRoute"),
                It.Is<object>(x => x.GetType().GetProperty("id").GetValue(x).ToString() == "1")), Times.Once);
        } 
    }
}