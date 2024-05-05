using Microsoft.AspNetCore.Http;
using NewsMedia.Domain.Models.Entities;

namespace NewsMedia.Infrastructure.DTOS.Entities.Article.Post
{
    public record AddArticleDto(int id, string Name, string Content, int? CategoryId, List<int> Tags, IFormFile ArticleTitleFile, List<IFormFile>? ArticleContentFiles, Language Language);
}
