using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsMedia.Infrastructure.DTOS.Entities.Category.Post;
using NewsMedia.Infrastructure.Services.Entities.Abstractions;

namespace NewsMediaApp.Controllers
{
    public class CategoryController : Controller
    {
        readonly ICategoryService _categoryService;
        readonly ICategoryBaseService _categoryBaseService;
        public CategoryController(ICategoryService categoryService, ICategoryBaseService categoryBaseService)
        {
            _categoryService = categoryService;
            _categoryBaseService = categoryBaseService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategories();
            return View(categories);
        }

        public IActionResult AddCategory()
        {
            var categories = _categoryBaseService.GetCategoryBases().Result;

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategory(id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryDto addCategoryDto)
        {

            if (ModelState.IsValid)
            {
                await _categoryService.AddCategory(addCategoryDto);
            }
            return RedirectToAction("AddCategory", "Category");
        }

        public IActionResult DeleteCategory()
        {
            return View();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategory(id);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult UpdateCategory()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var category = await _categoryService.GetCategoryWithBase(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryBases = await _categoryBaseService.GetCategoryBases();
            ViewBag.CategoryBases = new SelectList(categoryBases, "Id", "Name", category.CategoryBase.Id);
            return View(category);
        }
    }
}
