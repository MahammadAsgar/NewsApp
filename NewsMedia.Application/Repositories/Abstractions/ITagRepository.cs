using NewsMedia.Domain.Models.Entities;

namespace NewsMedia.Application.Repositories.Abstractions
{
    public interface ITagRepository
    {
        Task<Tag> GetTagWithArticle(int id);
        Task<Tag> GetTagsForDelete(int id);
    }
}
