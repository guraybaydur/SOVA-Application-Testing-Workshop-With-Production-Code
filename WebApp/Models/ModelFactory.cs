using System.Web.Http.Routing;
using WebApp.DomainModel;

namespace WebApp.Models
{
    public class ModelFactory
    {
        public static CategoryModel Map(Category category, UrlHelper urlHelper)
        {
            return new CategoryModel
            {
                Url = urlHelper.Link("CategoryRoute", new { id = category.Id }),
                Name = category.Name
            };
        }
    }
}