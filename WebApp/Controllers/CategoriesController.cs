using System.Web.Http;
using WebApp.DomainModel;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly ICategoryDataService _categoryService;

        public CategoriesController(ICategoryDataService categoryService)
        {
            _categoryService = categoryService;
        }

        public IHttpActionResult Get()
        {
            var categories = _categoryService.GetAll();
            return Ok(categories);
        }

        public IHttpActionResult Get(int id)
        {
            var category = _categoryService.GetById(id);

            if(category == null) return NotFound();

            var model = ModelFactory.Map(category, Url);

            return Ok(model);
        }

        public IHttpActionResult Post(CategoryModel categoryModel)
        {
            var category = new Category
            {
                Name = categoryModel.Name
            };
         
            _categoryService.Insert(category);

            return Created("CategoryRoute", ModelFactory.Map(category, Url));
        }


        public IHttpActionResult Delete(int id)
        {
            if(!_categoryService.Delete(id)) return NotFound();
            return Ok();
        }

        public IHttpActionResult Put(int id, CategoryModel categoryModel)
        {
            var category = new Category
            {
                Id = id,
                Name = categoryModel.Name
            };

            if(!_categoryService.Update(category)) return NotFound();

            return Ok();
        }


    }
}
