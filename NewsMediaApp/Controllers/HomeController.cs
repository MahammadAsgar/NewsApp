using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public HomeController(ILogger<HomeController> logger, IArticleService articleService, ICategoryService categoryService)
        {
            _logger = logger;
            _articleService = articleService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesWithArticlesAsync();
            var articles = await _articleService.GetArticles();
            var datas = new ArtcilesCategories()
            {
                Articles = articles,
                CategoriesArticles = categories
            };
            return View(datas);
        }

        [HttpGet]
        public async Task<IActionResult>  GetArticleById(int id)
        {
            var article = await _articleService.GetArticle(id);
            var articles= await _articleService.GetArticlesByTag(article.Category, article.Tags);
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
