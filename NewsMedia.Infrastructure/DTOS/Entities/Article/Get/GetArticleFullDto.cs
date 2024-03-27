using NewsMedia.Infrastructure.DTOS.Entities.ArticleFile;
using NewsMedia.Infrastructure.DTOS.Entities.Category.Get;
using NewsMedia.Infrastructure.DTOS.Entities.Tag.Get;

namespace NewsMedia.Infrastructure.DTOS.Entities.Article.Get
{
    public class GetArticleFullDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public GetCategoryDto Category { get; set; }
        public GetFile ArticleTitleFile { get; set; }
        public List<GetFile> ArticleContentFiles { get; set; }
        public List<GetTagDto> Tags { get; set; }
        public DateTime AddDate { get; set; }
    }
}