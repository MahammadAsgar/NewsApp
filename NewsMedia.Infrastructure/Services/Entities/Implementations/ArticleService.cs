using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsMedia.Application.Repositories.Abstractions;
using NewsMedia.Application.UnitOfWorks;
using NewsMedia.Domain.Models.Entities;
using NewsMedia.Domain.Models.Users;
using NewsMedia.Infrastructure.DTOS.Entities.Article.Get;
using NewsMedia.Infrastructure.DTOS.Entities.Article.Post;
using NewsMedia.Infrastructure.Services.Entities.Abstractions;
using NewsMedia.Infrastructure.Services.Storage;

namespace NewsMedia.Infrastructure.Services.Entities.Implementations
{
    public class ArticleService : IArticleService
    {
        readonly IMapper _mapper;
        readonly IGenericRepository<Article> _genericRepository;
        readonly IArticleRepository _articleRepository;
        readonly IUnitofWork _unitofWork;
        readonly IGenericRepository<Tag> _tagRepository;
        readonly IFilelStorage _filelStorage;
        readonly UserManager<AppUser> _userManager;
        public ArticleService(IMapper mapper, IGenericRepository<Article> genericRepository, IArticleRepository articleRepository,
            IUnitofWork unitofWork, IGenericRepository<Tag> tagRepository, IFilelStorage filelStorage, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _articleRepository = articleRepository;
            _unitofWork = unitofWork;
            _tagRepository = tagRepository;
            _filelStorage = filelStorage;
            _userManager = userManager;
        }
        public async Task AddArticle(AddArticleDto addArticleDto, string userId)
        {
            var entity = _mapper.Map<Article>(addArticleDto);
            var user = await _userManager.FindByIdAsync(userId);
            entity.AppUser = user;
            var tags = _tagRepository.Where(x => addArticleDto.Tags.Contains(x.Id)).ToList();
            entity.Tags = tags;
            entity.ArticleTitleFile = await _filelStorage.UploadArticleTitleFile(addArticleDto);
            await _genericRepository.AddAsync(entity);
            await _unitofWork.CommitAsync();
            await _filelStorage.UploadAsync(addArticleDto, entity);
        }


        public Task DeleteArticle(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetArticleFullDto> GetArticle(int id)
        {
            var entity = await _articleRepository.GetArticleFull(id);
            return _mapper.Map<GetArticleFullDto>(entity);
        }

        public async Task<IEnumerable<GetArticleFullDto>> GetArticles()
        {
            var entities = await _articleRepository.GetArticles();
            return _mapper.Map<IEnumerable<GetArticleFullDto>>(entities);
        }

        public async Task<IEnumerable<GetArticleDto>> GetArticlesFilter(string param)
        {
            var entities = await _articleRepository.Where(x => x.Name == param).ToListAsync();
            var data = _mapper.Map<IEnumerable<GetArticleDto>>(entities);
            return data;
        }

        public async Task<IEnumerable<GetArticleFullDto>> GetArticleByUser(int userId)
        {
            var entities = await _articleRepository.Where(x => x.AppUser.Id == userId).ToListAsync();
            return _mapper.Map<IEnumerable<GetArticleFullDto>>(entities);
        }
        public async Task<IEnumerable<GetArticleSlider>> GetArticleForSlider()
        {
            var entities = await _articleRepository.GetArticleSlider();
            return _mapper.Map<IEnumerable<GetArticleSlider>>(entities);
        }
        public async Task<GetArticleFullDto> UpdateArticle(AddArticleDto addCategoryDto, int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (!string.IsNullOrEmpty(addCategoryDto.Name))
            {
                entity.Name = addCategoryDto.Name;
            }
            //if (!string.IsNullOrEmpty(addCategoryDto.Description))
            //{
            //    entity.Description = addCategoryDto.Description;
            //}
            if (!string.IsNullOrEmpty(addCategoryDto.Content))
            {
                entity.Content = addCategoryDto.Content;
            }
            if (addCategoryDto.CategoryId.HasValue)
            {
                entity.CategoryId = addCategoryDto.CategoryId.Value;
            }
            /*if (addCategoryDto.TagIds.Count > 0)
            {
                var tags = new List<Tag>();

                foreach (var item in addCategoryDto.TagIds)
                {
                    tags.Add(await _tagRepository.GetByIdAsync(item));
                }
                entity.Tags.AddRange(tags);
            }*/
            _genericRepository.Update(entity);
            _unitofWork.Commit();
            return _mapper.Map<GetArticleFullDto>(entity);
        }
    }
}
