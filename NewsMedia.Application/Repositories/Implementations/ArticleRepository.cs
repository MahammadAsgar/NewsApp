using Microsoft.EntityFrameworkCore;
using NewsMedia.Application.Context;
using NewsMedia.Application.Repositories.Abstractions;
using NewsMedia.Domain.Models.Entities;
using System.Linq.Expressions;

namespace NewsMedia.Application.Repositories.Implementations
{
    public class ArticleRepository : IArticleRepository
    {
        readonly NewsDbContext _dbContext;
        readonly DbSet<Article> _dbsSet;
        public ArticleRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbsSet = _dbContext.Set<Article>();
        }
        public async Task<Article> GetArticleFull(int id)
        {
            return await _dbsSet
                .Include(x => x.Tags)
                .Include(x => x.Category)
                .Include(x => x.ArticleTitleFile)
                .Include(x => x.ArticleContentFiles)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Article>> GetArticles()
        {
            var articles = await _dbsSet
                .Include(x => x.Tags)
                .Include(x => x.Category)
                .Include(x => x.ArticleTitleFile)
                .Include(x => x.ArticleContentFiles)
                .Include(x => x.AppUser)
                .OrderBy(x => x.CategoryId)
                .ToListAsync();
            return articles;
        }
        public async Task<IEnumerable<Article>> GetArticleSlider()
        {
            return await _dbsSet
                .Include(x => x.ArticleTitleFile)
                .ToListAsync();
        }

        public IQueryable<Article> Where(Expression<Func<Article, bool>> predicate)
        {
            return _dbsSet
                .Include(x => x.Tags)
                .Include(x => x.Category)
                .Include(x => x.ArticleTitleFile)
                .Include(x => x.ArticleContentFiles)
                .OrderBy(x => x.AddDate)
                .Where(predicate);
        }

        public async Task<IEnumerable<Article>> ArticlesByUser(int userId)
        {
            return await _dbsSet
                .Include(x => x.Tags)
                .Include(x => x.Category)
                .Include(x => x.ArticleTitleFile)
                .OrderBy(x => x.AddDate)
                .Where(x => x.AppUser.Id == userId)
                .ToListAsync();
        }
    }
}
