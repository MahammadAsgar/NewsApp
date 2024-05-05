using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsMedia.Application.Repositories.Abstractions;
using NewsMedia.Application.UnitOfWorks;
using NewsMedia.Domain.Models.Entities;
using NewsMedia.Infrastructure.DTOS.Entities.Tag.Get;
using NewsMedia.Infrastructure.DTOS.Entities.Tag.Post;
using NewsMedia.Infrastructure.Services.Entities.Abstractions;

namespace NewsMedia.Infrastructure.Services.Entities.Implementations
{
    public class TagService : ITagService
    {
        readonly IUnitofWork _unitofWork;
        readonly IGenericRepository<Tag> _genericRepository;
        readonly IMapper _mapper;
        readonly ITagRepository _tagRepository;
        readonly IArticleRepository _articleRepository;
        public TagService(IMapper mapper, IUnitofWork unitofWork, IGenericRepository<Tag> genericRepository, ITagRepository tagRepository, IArticleRepository articleRepository)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _unitofWork = unitofWork;
            _tagRepository = tagRepository;
            _articleRepository = articleRepository;
        }
        public async Task AddTag(AddTagDto addTagDto)
        {
            var entity = _mapper.Map<Tag>(addTagDto);
            await _genericRepository.AddAsync(entity);
            await _unitofWork.CommitAsync();
        }

        public async Task DeleteTag(int id)
        {
            //var tag = await _tagRepository.GetTagsForDelete(id);
            //var articles = await _articleRepository.Where(x => x.Tags.Contains(tag)).ToListAsync();
        }

        public async Task<GetTagWithArticlesDto> GetTag(int id, Language language)
        {
            var entity = await _tagRepository.GetTagWithArticle(id, language);
            return _mapper.Map<GetTagWithArticlesDto>(entity);
        }

        public async Task<IEnumerable<GetTagDto>> GetTags(Language language)
        {
            var enyities = await _genericRepository.GetEntities();
            var tags = _mapper.Map<IEnumerable<GetTagDto>>(enyities);
            return tags;
        }

        public async Task<GetTagDto> UpdateTag(AddTagDto addTagDto, int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (!string.IsNullOrEmpty(addTagDto.Name))
            {
                entity.Name = addTagDto.Name;
                _genericRepository.Update(entity);
                _unitofWork.Commit();
            }
            return _mapper.Map<GetTagDto>(entity);
        }
    }
}
