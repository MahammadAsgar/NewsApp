using NewsMedia.Infrastructure.DTOS.Entities.Category.Get;
using NewsMedia.Infrastructure.DTOS.Entities.Category.Post;

namespace NewsMedia.Infrastructure.Services.Entities.Abstractions
{
    public interface ICategoryService
    {
        Task AddCategory(AddCategoryDto addCategoryDto);
        Task<GetCategoryDto> UpdateCategory(AddCategoryDto addCategoryDto, int id);
        Task DeleteCategory(int id);
        Task<GetCategoryWithArticleDto> GetCategory(int id);
        Task<IEnumerable<GetCategoryDto>> GetCategories();
        Task<IEnumerable<GetCategoryWithArticleDto>> GetAllCategoriesWithArticlesAsync();
        Task<GetCatgoryWithBaseDto> GetCategoryWithBase(int id);
    }
}
