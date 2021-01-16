using HackathonCCR.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HackathonCCR.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var result = _categoryService.GetCategorySelectList();
            return Json(result, new JsonSerializerSettings());
        }
    }
}
