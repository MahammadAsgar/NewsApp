using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsMedia.Infrastructure.DTOS.Entities.Article.Post;
using NewsMedia.Infrastructure.Services.Entities.Abstractions;
using System.Security.Claims;

namespace NewsMediaApp.Controllers
{
    public class NewsController : Controller
    {
        readonly IArticleService _articleService;
        readonly ITagService _tagService;
        readonly ICategoryService _categoryService;
        readonly IHttpContextAccessor _contextAccessor;
        public NewsController(ITagService tagService, ICategoryService categoryService, IArticleService articleService, IHttpContextAccessor contextAccessor)
        {
            _tagService = tagService;
            _articleService = articleService;
            _categoryService = categoryService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _articleService.GetArticles();
            return View(articles);
        }

        public async Task<IActionResult> GetArticle(int id)
        {
            var article = await _articleService.GetArticle(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }



        public IActionResult AddArticle()
        {
            var categories = _categoryService.GetCategories().Result;

            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var tags = _tagService.GetTags().Result.ToList();

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
            return RedirectToAction("Index", "News");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateArticle(int id)
        {
            // Retrieve the current article data
            var article = await _articleService.GetArticle(id);
            if (article == null)
            {
                return NotFound();
            }

            // Retrieve categories and tags
            var categories = await _categoryService.GetCategories();
            var tags = await _tagService.GetTags();

            // Select the tags that are already assigned to the article
            var selectedTags = article.Tags.Select(t => t.Id).ToList();

            // Pass the article data and selected tags to the view
            ViewBag.Categories = new SelectList(categories, "Id", "Name", article.Category.Id);
            ViewBag.Tags = new MultiSelectList(tags, "Id", "Name", selectedTags);

            return View(article);
        }
    }
}
