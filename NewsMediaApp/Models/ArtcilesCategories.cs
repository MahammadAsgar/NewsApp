using NewsMedia.Infrastructure.DTOS.Entities.Article.Get;
using NewsMedia.Infrastructure.DTOS.Entities.Category.Get;

namespace NewsMediaApp.Models
{
    public class ArtcilesCategories
    {
        public IEnumerable<GetArticleFullDto> Articles { get; set; }
        public IEnumerable<GetCategoryWithArticleDto> CategoriesArticles { get; set; }
    }
}
