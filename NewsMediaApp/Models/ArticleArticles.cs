using NewsMedia.Infrastructure.DTOS.Entities.Article.Get;

namespace NewsMediaApp.Models
{
    public class ArticleArticles
    {
        public GetArticleFullDto Article  { get; set; }
        public IEnumerable<GetArticleFullDto> Articles  { get; set; }
    }
}
