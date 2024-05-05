using NewsMedia.Domain.Models.Entities;

namespace NewsMedia.Application.Repositories.Abstractions
{
    public interface ITagRepository
    {
        Task<Tag> GetTagWithArticle(int id, Language language);
        Task<Tag> GetTagsForDelete(int id, Language language);
    }
}
