using Microsoft.EntityFrameworkCore;
using NewsMedia.Application.Context;
using NewsMedia.Application.Repositories.Abstractions;
using NewsMedia.Domain.Models.Entities;

namespace NewsMedia.Application.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        readonly NewsDbContext _dbContext;
        readonly DbSet<Category> _dbSet;
        public CategoryRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Category>();
        }

        public async Task<Category> GetCategoryWithArticle(int id)
        {
            return await _dbSet
                 .Include(x => x.Articles)
                 .ThenInclude(y => y.ArticleTitleFile)
                 .Include(x => x.Articles)
                 .ThenInclude(y => y.Tags)
                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> GetCategoryWithBase(int id)
        {
            return await _dbSet.Include(x => x.CategoryBase).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesWithArticlesAsync()
        {
            return await _dbSet
                .Include(x => x.Articles)
                .ThenInclude(y => y.ArticleTitleFile)
                .Include(x => x.Articles)
                .ThenInclude(y => y.Tags)
                .OrderBy(x => x.Id)
                .Where(x => x.Articles.Count() >=1)
                .ToListAsync();
        }
    }
}
