using NewsMedia.Domain.Models.Base;

namespace NewsMedia.Domain.Models.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Article> Articles { get; set; }
        public int CategoryBaseId { get; set; }
        public CategoryBase CategoryBase { get; set; }
        public Language Language { get; set; }
    }
}
