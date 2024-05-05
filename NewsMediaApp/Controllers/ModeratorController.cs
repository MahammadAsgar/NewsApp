using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsMedia.Domain.Models.Entities;
using NewsMedia.Infrastructure.DTOS.Entities.Article.Post;
using NewsMedia.Infrastructure.Services.Entities.Abstractions;
using System.Security.Claims;

namespace NewsMediaApp.Controllers
{
    public class ModeratorController : Controller
    {
        readonly IArticleService _articleService;
        readonly ICategoryService _categoryService;
        readonly ITagService _tagService;
        readonly IHttpContextAccessor _contextAccessor;
        public ModeratorController(ICategoryService categoryService, IArticleService articleService, ITagService tagService, IHttpContextAccessor contextAccessor)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _tagService = tagService;
            _contextAccessor = contextAccessor;
        }

        #region news
        //add news
        //update news
        //delete news
        #endregion


        public IActionResult AddArticle(Language language)
        {
            var categories = _categoryService.GetCategories(language).Result;

            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var tags = _tagService.GetTags(language).Result.ToList();

            ViewBag.Tags = new MultiSelectList(tags, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle(AddArticleDto addArticleDto)
        {
            var user = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                await _articleService.AddArticle(addArticleDto, user);
            }
            return RedirectToAction("GetAllArticles", "Moderator");
        }


        //[HttpGet]
        //public async Task<IActionResult> UpdateArticle(int id)
        //{
        //    // Retrieve the current article data
        //    var article = await _articleService.GetArticle(id);
        //    if (article == null)
        //    {
        //        return NotFound();
        //    }

        //    // Retrieve categories and tags
        //    var categories = await _categoryService.GetCategories();
        //    var tags = await _tagService.GetTags();

        //    // Select the tags that are already assigned to the article
        //    var selectedTags = article.Tags.Select(t => t.Id).ToList();

        //    // Pass the article data and selected tags to the view
        //    ViewBag.Categories = new SelectList(categories, "Id", "Name", article.Category.Id);
        //    ViewBag.Tags = new MultiSelectList(tags, "Id", "Name", selectedTags);

        //    return View(article);
        //}

        [HttpGet]
        public async Task<IActionResult> GetAllArticles(Language language)
        {
            var articles = await _articleService.GetArticles(language);
            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserArticles(Language language)
        {
            var user = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var articles = await _articleService.GetArticleByUser(Int32.Parse(user), language);
            return View(articles);
        }

        public async Task<IActionResult> Home(Language language)
        {
            var user = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var articles = await _articleService.GetArticleByUser(Int32.Parse(user), language);
            return View(articles);
        }
    }
}
