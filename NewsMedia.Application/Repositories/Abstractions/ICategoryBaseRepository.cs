using NewsMedia.Domain.Models.Entities;

namespace NewsMedia.Application.Repositories.Abstractions
{
    public interface ICategoryBaseRepository
    {
        Task<CategoryBase> GetCategoryBaseWithCategory(int id, Language language);
        Task<List<CategoryBase>> GetCategoryBaseWithCategories(Language language);
    }
}
