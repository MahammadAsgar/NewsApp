using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NewsMedia.Domain.Models.Entities;
using NewsMedia.Infrastructure.Services.Entities.Abstractions;
using NewsMediaApp.Models;
using System.Diagnostics;
namespace NewsMediaApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly IArticleService _articleService;
        readonly ICategoryBaseService _categoryBaseService;
        readonly ICategoryService _categoryService;
        readonly IStringLocalizer<HomeController> _localizer;
        public HomeController(ILogger<HomeController> logger, IArticleService articleService, ICategoryService categoryService, IStringLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _articleService = articleService;
            _categoryService = categoryService;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index(Language language)
        {
            var categories = await _categoryService.GetAllCategoriesWithArticlesAsync(language);
            var articles = await _articleService.GetArticles(language);
            var datas = new ArtcilesCategories()
            {
                Articles = articles,
                CategoriesArticles = categories
            };
            return View(datas);
        }

        [HttpGet]
        public async Task<IActionResult> GetArticleById(int id, Language language)
        {
            var article = await _articleService.GetArticle(id, language);
            var articles = await _articleService.GetArticlesByTag(article.Category, article.Tags);
            var datas = new ArticleArticles
            {
                Article = article,
                Articles = articles
            };
            return View(datas);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
