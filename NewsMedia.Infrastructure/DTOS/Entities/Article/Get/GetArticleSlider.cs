using NewsMedia.Infrastructure.DTOS.Entities.ArticleFile;

namespace NewsMedia.Infrastructure.DTOS.Entities.Article.Get
{
    public class GetArticleSlider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GetFile TitleFile { get; set; }
        public DateTime AddDate { get; set; }
    }
}
