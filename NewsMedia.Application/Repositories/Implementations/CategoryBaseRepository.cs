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
        public async Task<CategoryBase> GetCategoryBaseWithCategory(int id)
        {
            return await _dbSet
                .Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CategoryBase>> GetCategoryBaseWithCategories()
        {
            return await _dbSet
                .Include(x => x.Categories)
                .ToListAsync();
        }
    }
}
