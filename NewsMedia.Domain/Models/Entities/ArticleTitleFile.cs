using NewsMedia.Domain.Models.Base;

namespace NewsMedia.Domain.Models.Entities
{
    public class ArticleTitleFile : ArticleFile
    {
        public Article Article { get; set; }
    }
}
