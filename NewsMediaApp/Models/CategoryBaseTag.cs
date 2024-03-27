using NewsMedia.Infrastructure.DTOS.Entities.CategoryBase.Get;
using NewsMedia.Infrastructure.DTOS.Entities.Tag.Get;

namespace NewsMediaApp.Models
{
    public class CategoryBaseTag
    {
        public IEnumerable<GetTagDto> Tags { get; set; }
        public IEnumerable<GetCategoryBaseWithCategoriesDto> CategoryBases { get; set; }
    }
}
