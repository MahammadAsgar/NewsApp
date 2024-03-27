using NewsMedia.Infrastructure.DTOS.Entities.Category.Get;

namespace NewsMedia.Infrastructure.DTOS.Entities.CategoryBase.Get
{
    public record GetCategoryBaseWithCategoriesDto(int Id, string Name, List<GetCategoryDto> Categories);
}
