using Microsoft.AspNetCore.Mvc;
using NewsMedia.Infrastructure.DTOS.Entities.Tag.Post;
using NewsMedia.Infrastructure.Services.Entities.Abstractions;

namespace NewsMediaApp.Controllers
{
    public class TagController : Controller
    {
        readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            var tags = await _tagService.GetTags();
            return View(tags);
        }

        public async Task<IActionResult> GetTag(int id)
        {
            var tag = await _tagService.GetTag(id);
            return View(tag);
        }

        public IActionResult AddTag()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTag(AddTagDto addTagDto)
        {
            if (ModelState.IsValid)
            {
                await _tagService.AddTag(addTagDto);
            }
            return RedirectToAction("Index", "Tag");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTag(int id)
        {
            var tag = await _tagService.GetTag(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }
    }
}
