using Microsoft.AspNetCore.Mvc;
using NewsMedia.Domain.Models.Entities;
using NewsMedia.Infrastructure.Services.Entities.Abstractions;

namespace NewsMediaApp.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        readonly ICategoryBaseService _categoryBaseService;
        public CategoryViewComponent(ICategoryBaseService categoryBaseService)
        {
            _categoryBaseService = categoryBaseService;
        }
        public async Task<IViewComponentResult> InvokeAsync(Language language)
        {
            var categoryBases = await _categoryBaseService.GetCategoryBasesWithCategories(language);
            return View(categoryBases);
        }
    }
}
