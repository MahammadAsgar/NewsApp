using NewsMedia.Domain.Models.Entities;

namespace NewsMedia.Application.Repositories.Abstractions
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryWithArticle(int id, Language language);
        Task<IEnumerable<Category>> GetAllCategoriesWithArticlesAsync(Language language);
        Task<Category> GetCategoryWithBase(int id, Language language);
    }
}
