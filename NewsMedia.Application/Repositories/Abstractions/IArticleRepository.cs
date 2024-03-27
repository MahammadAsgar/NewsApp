using NewsMedia.Domain.Models.Entities;
using System.Linq.Expressions;

namespace NewsMedia.Application.Repositories.Abstractions
{
    public interface IArticleRepository
    {
        Task<Article> GetArticleFull(int id);
        Task<IEnumerable<Article>> GetArticles();
        Task<IEnumerable<Article>> GetArticleSlider();
        IQueryable<Article> Where(Expression<Func<Article, bool>> predicate);
        Task<IEnumerable<Article>> ArticlesByUser(int userId);
    }
}
