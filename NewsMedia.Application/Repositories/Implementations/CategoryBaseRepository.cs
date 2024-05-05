using Microsoft.EntityFrameworkCore;
using NewsMedia.Application.Context;
using NewsMedia.Application.Repositories.Abstractions;
using NewsMedia.Domain.Models.Entities;

namespace NewsMedia.Application.Repositories.Implementations
{
    public class CategoryBaseRepository : ICategoryBaseRepository
    {
        readonly NewsDbContext _dbContext;
        readonly DbSet<CategoryBase> _dbSet;
        public CategoryBaseRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<CategoryBase>();
        }
        public async Task<CategoryBase> GetCategoryBaseWithCategory(int id, Language language)
        {
            return await _dbSet
                .Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == id&&x.Language==language);
        }

        public async Task<List<CategoryBase>> GetCategoryBaseWithCategories(Language language)
        {
            return await _dbSet
                .Include(x => x.Categories)
                .Where(x=>x.Language== language)
                .ToListAsync();
        }
    }
}
