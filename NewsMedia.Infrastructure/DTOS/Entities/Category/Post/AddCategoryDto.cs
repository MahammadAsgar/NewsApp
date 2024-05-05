using NewsMedia.Domain.Models.Entities;

namespace NewsMedia.Infrastructure.DTOS.Entities.Category.Post
{
    public record AddCategoryDto(string Name, int CategoryBaseId, Language Language);
}
