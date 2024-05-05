using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsMedia.Domain.Models.Entities;
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
        public async Task<IActionResult> Index(Language language)
        {
            var categories = await _categoryService.GetCategories(language);
            return View(categories);
        }

        public IActionResult AddCategory(Language language)
        {
            var categories = _categoryBaseService.GetCategoryBases(language).Result;

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        public async Task<IActionResult> GetCategory(int id, Language language)
        {
            var category = await _categoryService.GetCategory(id, language);
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
        public async Task<IActionResult> UpdateCategory(int id, Language language)
        {
            var category = await _categoryService.GetCategoryWithBase(id, language);
            if (category == null)
            {
                return NotFound();
            }
            var categoryBases = await _categoryBaseService.GetCategoryBases(language);
            ViewBag.CategoryBases = new SelectList(categoryBases, "Id", "Name", category.CategoryBase.Id);
            return View(category);
        }
    }
}
