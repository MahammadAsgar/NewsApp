using Microsoft.AspNetCore.Mvc;
using NewsMedia.Domain.Models.Entities;
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

        public async Task<IViewComponentResult> InvokeAsync(Language language)
        {
            var tags = await _tagService.GetTags(language);
            return View(tags);
        }
    }
}
