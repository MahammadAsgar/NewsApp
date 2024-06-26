﻿using NewsMedia.Infrastructure.DTOS.Entities.Tag.Get;
using NewsMedia.Infrastructure.DTOS.Entities.Tag.Post;

namespace NewsMedia.Infrastructure.Services.Entities.Abstractions
{
    public interface ITagService
    {
        Task AddTag(AddTagDto addTagDto);
        Task<GetTagDto> UpdateTag(AddTagDto addTagDto, int id);
        Task DeleteTag(int id);
        Task<GetTagWithArticlesDto> GetTag(int id);
        Task<IEnumerable<GetTagDto>> GetTags();
    }
}
