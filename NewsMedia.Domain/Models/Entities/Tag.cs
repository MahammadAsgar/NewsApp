using NewsMedia.Domain.Models.Base;

namespace NewsMedia.Domain.Models.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Article> Articles { get; set; }
    }
}
