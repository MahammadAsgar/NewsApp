using NewsMedia.Domain.Models.Entities;

namespace NewsMedia.Application.Repositories.Abstractions
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryWithArticle(int id);
        Task<IEnumerable<Category>> GetAllCategoriesWithArticlesAsync();
        Task<Category> GetCategoryWithBase(int id);
    }
}
