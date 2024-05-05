using Microsoft.AspNetCore.Mvc;
using NewsMedia.Domain.Models.Entities;
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
        public async Task<IViewComponentResult> InvokeAsync(Language language)
        {
            var articles = await _articleService.GetArticles(language);
            return View(articles);
        }
    }
}
