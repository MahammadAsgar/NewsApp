using NewsMedia.Domain.Models.Entities;

namespace NewsMedia.Application.Repositories.Abstractions
{
    public interface ICategoryBaseRepository
    {
        Task<CategoryBase> GetCategoryBaseWithCategory(int id);
        Task<List<CategoryBase>> GetCategoryBaseWithCategories();
    }
}
