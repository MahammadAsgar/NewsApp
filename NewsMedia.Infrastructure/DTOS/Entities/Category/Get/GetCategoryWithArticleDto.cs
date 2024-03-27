using NewsMedia.Infrastructure.DTOS.Entities.Article.Get;

namespace NewsMedia.Infrastructure.DTOS.Entities.Category.Get
{
    public record GetCategoryWithArticleDto(int id, string Name, List<GetArticleFullDto> Articles);
}
