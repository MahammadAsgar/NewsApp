using NewsMedia.Domain.Models.Entities;
using System.Linq.Expressions;

namespace NewsMedia.Application.Repositories.Abstractions
{
    public interface IArticleRepository
    {
        Task<Article> GetArticleFull(int id, Language language);
        Task<IEnumerable<Article>> GetArticles(Language language);
        Task<IEnumerable<Article>> GetArticleSlider(Language language);
        IQueryable<Article> Where(Expression<Func<Article, bool>> predicate);
        Task<IEnumerable<Article>> ArticlesByUser(int userId, Language language);
    }
}
