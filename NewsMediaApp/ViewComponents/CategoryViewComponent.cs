using Microsoft.AspNetCore.Mvc;
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
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoryBases = await _categoryBaseService.GetCategoryBasesWithCategories();
            return View(categoryBases);
        }
    }
}
