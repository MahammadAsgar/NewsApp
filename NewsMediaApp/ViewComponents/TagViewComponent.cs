using Microsoft.AspNetCore.Mvc;
using NewsMedia.Infrastructure.Services.Entities.Abstractions;

namespace NewsMediaApp.ViewComponents
{
    public class TagViewComponent : ViewComponent
    {
        readonly ITagService _tagService;
        public TagViewComponent(ITagService tagService)
        {
            _tagService = tagService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tags = await _tagService.GetTags();
            return View(tags);
        }
    }
}
