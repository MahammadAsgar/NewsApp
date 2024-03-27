using Microsoft.AspNetCore.Http;

namespace NewsMedia.Infrastructure.DTOS.Entities.Article.Post
{
    public record AddArticleDto(int id, string Name, string Content, int? CategoryId, List<int> Tags, IFormFile ArticleTitleFile, List<IFormFile>? ArticleContentFiles);
}
