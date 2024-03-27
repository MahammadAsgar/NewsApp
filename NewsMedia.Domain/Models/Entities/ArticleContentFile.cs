using NewsMedia.Domain.Models.Base;

namespace NewsMedia.Domain.Models.Entities
{
    public class ArticleContentFile : ArticleFile
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
