using NewsMedia.Domain.Models.Entities;
using NewsMedia.Infrastructure.DTOS.Entities.Category.Get;
using NewsMedia.Infrastructure.DTOS.Entities.Category.Post;

namespace NewsMedia.Infrastructure.Services.Entities.Abstractions
{
    public interface ICategoryService
    {
        Task AddCategory(AddCategoryDto addCategoryDto);
        Task<GetCategoryDto> UpdateCategory(AddCategoryDto addCategoryDto, int id);
        Task DeleteCategory(int id);
        Task<GetCategoryWithArticleDto> GetCategory(int id, Language language);
        Task<IEnumerable<GetCategoryDto>> GetCategories(Language language);
        Task<IEnumerable<GetCategoryWithArticleDto>> GetAllCategoriesWithArticlesAsync(Language language);
        Task<GetCatgoryWithBaseDto> GetCategoryWithBase(int id, Language language);
    }
}
