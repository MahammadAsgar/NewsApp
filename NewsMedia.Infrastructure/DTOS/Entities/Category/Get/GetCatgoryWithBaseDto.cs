using NewsMedia.Infrastructure.DTOS.Entities.CategoryBase.Get;

namespace NewsMedia.Infrastructure.DTOS.Entities.Category.Get
{
    public record GetCatgoryWithBaseDto(int Id, string Name, GetCategoryBaseDto CategoryBase);
}
