using NewsMedia.Domain.Models.Base;

namespace NewsMedia.Domain.Models.Entities
{
    public class CategoryBase : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Category> Categories { get; set; }
    }
}
