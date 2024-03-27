using NewsMedia.Infrastructure.DTOS.Entities.Article.Get;

namespace NewsMedia.Infrastructure.DTOS.Entities.Tag.Get
{
    public record GetTagWithArticlesDto(int id, string Name, List<GetArticleFullDto> Articles);
}
