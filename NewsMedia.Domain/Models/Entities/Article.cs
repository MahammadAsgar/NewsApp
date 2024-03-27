using NewsMedia.Domain.Models.Base;
using NewsMedia.Domain.Models.Users;

namespace NewsMedia.Domain.Models.Entities
{
    public class Article : BaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual List<Tag> Tags { get; set; }
        public int ArticleTitleFileId { get; set; }
        public ArticleTitleFile ArticleTitleFile { get; set; }
        public virtual List<ArticleContentFile>? ArticleContentFiles { get; set; }
        public AppUser AppUser { get; set; }
    }
}
