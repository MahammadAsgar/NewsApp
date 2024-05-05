using Microsoft.EntityFrameworkCore;
using NewsMedia.Application.Context;
using NewsMedia.Application.Repositories.Abstractions;
using NewsMedia.Domain.Models.Entities;

namespace NewsMedia.Application.Repositories.Implementations
{
    public class TagRepository : ITagRepository
    {
        readonly NewsDbContext _dbContext;
        readonly DbSet<Tag> _dbSet;

        public TagRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Tag>();
        }
        public async Task<Tag> GetTagWithArticle(int id, Language language)
        {
            return await _dbSet
                .Include(x => x.Articles)
                .ThenInclude(y=>y.ArticleTitleFile)
                .Include(x=>x.Articles)
                .ThenInclude(y => y.Tags)
                .FirstOrDefaultAsync(x => x.Id == id && x.Language == language);
        }

        public async Task<Tag> GetTagsForDelete(int id, Language language)
        {
            return await _dbSet.Include(x => x.Articles).FirstOrDefaultAsync(x => x.Id == id&&x.Language==language);
        }
    }
}
