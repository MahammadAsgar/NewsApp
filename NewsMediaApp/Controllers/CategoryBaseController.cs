using Microsoft.AspNetCore.Mvc;
using NewsMedia.Infrastructure.DTOS.Entities.CategoryBase.Post;
using NewsMedia.Infrastructure.Services.Entities.Abstractions;

namespace NewsMediaApp.Controllers
{
    public class CategoryBaseController : Controller
    {
        readonly ICategoryBaseService _categoryBaseService;
        public CategoryBaseController(ICategoryBaseService categoryBaseService)
        {
            _categoryBaseService = categoryBaseService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCategoryBase()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategoryBase(AddCategoryBaseDto addCategoryDto)
        {

            if (ModelState.IsValid)
            {
                await _categoryBaseService.AddCategoryBase(addCategoryDto);
            }
            return RedirectToAction("AddCategoryBase", "CategoryBase");
        }

        //[HttpGet]
        //public async Task<IActionResult> UpdateCategory(int id)
        //{
        //    var categoryBase = await _categoryBaseService.GetCategoryBase(id);
        //    if (categoryBase == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(categoryBase);
        //}
    }
}
