using NewsMedia.Domain.Models.Entities;
using NewsMedia.Infrastructure.DTOS.Entities.Article.Post;

namespace NewsMedia.Infrastructure.Services.Storage
{
    public interface IFilelStorage
    {
        Task UploadAsync(AddArticleDto addArticleDto, Article article);
        Task<ArticleTitleFile> UploadArticleTitleFile(AddArticleDto addArticleDto);
    }
}
