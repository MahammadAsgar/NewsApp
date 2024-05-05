

using NewsMedia.Domain.Models.Entities;
using NewsMedia.Infrastructure.DTOS.Entities.CategoryBase.Get;
using NewsMedia.Infrastructure.DTOS.Entities.CategoryBase.Post;

namespace NewsMedia.Infrastructure.Services.Entities.Abstractions
{
    public interface ICategoryBaseService
    {
        Task AddCategoryBase(AddCategoryBaseDto addCategoryBaseDto);
        Task<GetCategoryBaseDto> UpdateCategoryBase(AddCategoryBaseDto addCategoryBaseDto, int id);
        Task DeleteCategoryBase(int id);
        Task<GetCategoryBaseWithCategoriesDto> GetCategoryBase(int id, Language language);
        Task<IEnumerable<GetCategoryBaseWithCategoriesDto>> GetCategoryBasesWithCategories(Language language);
        Task<IEnumerable<GetCategoryBaseDto>> GetCategoryBases(Language language);

    }
}
