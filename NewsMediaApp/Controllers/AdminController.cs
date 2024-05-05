using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using NewsMedia.Domain.Models.Entities;
using NewsMedia.Infrastructure.DTOS.Entities.Category.Post;
using NewsMedia.Infrastructure.DTOS.Entities.CategoryBase.Post;
using NewsMedia.Infrastructure.DTOS.Entities.Tag.Post;
using NewsMedia.Infrastructure.Services.Entities.Abstractions;
using NewsMedia.Infrastructure.Services.Users.Abstractions;

namespace NewsMediaApp.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        readonly ITagService _tagService;
        readonly ICategoryBaseService _categoryBaseService;
        readonly ICategoryService _categoryService;
        readonly IArticleService _articleService;
        readonly IUserService _userService;
        public AdminController(ITagService tagService, ICategoryService categoryService, ICategoryBaseService categoryBaseService,
            IArticleService articleService, IUserService userService)
        {
            _tagService = tagService;
            _categoryService = categoryService;
            _categoryBaseService = categoryBaseService;
            _articleService = articleService;
            _userService = userService;
        }

        #region news
        //add news
        //update news
        //delete news
        #endregion

        #region tags
        public IActionResult AddTag()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTag(AddTagDto addTagDto)
        {
            if (ModelState.IsValid)
            {
                await _tagService.AddTag(addTagDto);
            }
            return RedirectToAction("Index", "Tag");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTag(int id)
        {
            //var tag = await _tagService.GetTag(id);
            //if (tag == null)
            //{
            //    return NotFound();
            //}
            //return View(tag);
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTag(int id)
        {
         
            return RedirectToAction("Index", "Tag");
        }
        #endregion

        #region categorybases
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
            return RedirectToAction("AddCategoryBase");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategoryBase(int id)
        {
            //var categoryBase = await _categoryBaseService.GetCategoryBase(id);
            //if (categoryBase == null)
            //{
                return NotFound();
            //}
            //return View(categoryBase);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryBase(int id)
        {
            //var categoryBase = await _categoryBaseService.GetCategoryBase(id);
            //if (categoryBase == null)
            //{
            //    return NotFound();
            //}
            //_categoryBaseService.DeleteCategoryBase(id);
            return RedirectToAction("AddCategoryBase", "CategoryBase");
        }

        #endregion

        #region categories
        public IActionResult AddCategory(Language language)
        {
            var categories = _categoryBaseService.GetCategoryBases(language).Result;

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryDto addCategoryDto)
        {

            if (ModelState.IsValid)
            {
                await _categoryService.AddCategory(addCategoryDto);
            }
            return RedirectToAction("AddCategory", "Admin");
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

        //[HttpGet]
        //public async Task<IActionResult> UpdateCategory(int id)
        //{
        //    var category = await _categoryService.GetCategoryWithBase(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    var categoryBases = await _categoryBaseService.GetCategoryBases();
        //    ViewBag.CategoryBases = new SelectList(categoryBases, "Id", "Name", category.CategoryBase.Id);
        //    return View(category);
        //}
        #endregion

        #region users     

        [HttpPost]
        public async Task<IActionResult> ToggleModerator(int userId)
        {
            await _userService.ToggleModerator(userId);
            return RedirectToAction("UserList");
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var users = await _userService.Users();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> UserArticles(int userId, Language language)
        {
            var articles = await _articleService.GetArticleByUser(userId, language);
            return View(articles);
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Home(Language language)
        {
            var articles = await _articleService.GetArticles(language);
            return View(articles);
        }
    }
}
