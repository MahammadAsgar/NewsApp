using Microsoft.AspNetCore.Mvc;
using NewsMedia.Infrastructure.Services.Entities.Abstractions;

namespace NewsMediaApp.ViewComponents
{
    public class ArticlesViewComponent : ViewComponent
    {
        readonly IArticleService _articleService;
        public ArticlesViewComponent(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articles = await _articleService.GetArticles();
            return View(articles);
        }
    }
}
