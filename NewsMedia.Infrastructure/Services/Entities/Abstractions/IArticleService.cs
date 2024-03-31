using NewsMedia.Infrastructure.DTOS.Entities.Article.Get;
using NewsMedia.Infrastructure.DTOS.Entities.Article.Post;
using NewsMedia.Infrastructure.DTOS.Entities.Category.Get;
using NewsMedia.Infrastructure.DTOS.Entities.Tag.Get;

namespace NewsMedia.Infrastructure.Services.Entities.Abstractions
{
    public interface IArticleService
    {
        Task AddArticle(AddArticleDto addArticleDto, string user);
        Task<GetArticleFullDto> UpdateArticle(AddArticleDto addCategoryDto, int id);
        Task DeleteArticle(int id);
        Task<GetArticleFullDto> GetArticle(int id);
        Task<IEnumerable<GetArticleFullDto>> GetArticles();
        Task<IEnumerable<GetArticleDto>> GetArticlesFilter(string param);
        Task<IEnumerable<GetArticleSlider>> GetArticleForSlider();
        Task<IEnumerable<GetArticleFullDto>> GetArticleByUser(int userId);
        Task<IEnumerable<GetArticleFullDto>> GetArticlesByTag(GetCategoryDto categoryDto, List<GetTagDto> tagDtos);
    }
}
