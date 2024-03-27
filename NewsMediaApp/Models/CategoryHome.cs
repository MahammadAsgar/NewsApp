using NewsMedia.Infrastructure.DTOS.Entities.Article.Get;
using NewsMedia.Infrastructure.DTOS.Entities.CategoryBase.Get;

namespace NewsMediaApp.Models
{
    public class CategoryHome
    {
        public List<GetArticleFullDto> Articles { get; set; }
        public List<GetCategoryBaseWithCategoriesDto> Categories { get; set; }
    }
}
