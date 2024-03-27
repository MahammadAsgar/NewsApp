using AutoMapper;
using NewsMedia.Application.Repositories.Abstractions;
using NewsMedia.Application.UnitOfWorks;
using NewsMedia.Domain.Models.Entities;
using NewsMedia.Infrastructure.DTOS.Entities.Category.Get;
using NewsMedia.Infrastructure.DTOS.Entities.Category.Post;
using NewsMedia.Infrastructure.Services.Entities.Abstractions;

namespace NewsMedia.Infrastructure.Services.Entities.Implementations
{
    public class CategoryService : ICategoryService
    {
        readonly IMapper _mapper;
        readonly IGenericRepository<Category> _genericRepository;
        readonly IUnitofWork _unitofWork;
        readonly ICategoryRepository _categoryRepository;
        public CategoryService(IMapper mapper, IGenericRepository<Category> genericRepository, IUnitofWork unitofWork, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _unitofWork = unitofWork;
            _categoryRepository = categoryRepository;
        }
        public async Task AddCategory(AddCategoryDto addCategoryDto)
        {
            var entity = _mapper.Map<Category>(addCategoryDto);
            await _genericRepository.AddAsync(entity);
            await _unitofWork.CommitAsync();
        }

        public Task DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GetCategoryWithArticleDto>> GetAllCategoriesWithArticlesAsync()
        {
            var entities = await _categoryRepository.GetAllCategoriesWithArticlesAsync();
            return _mapper.Map<IEnumerable<GetCategoryWithArticleDto>>(entities);
        }
        public async Task<IEnumerable<GetCategoryDto>> GetCategories()
        {
            var entities = await _genericRepository.GetEntities();
            return _mapper.Map<IEnumerable<GetCategoryDto>>(entities);
        }

        public async Task<GetCategoryWithArticleDto> GetCategory(int id)
        {
            var entity = await _categoryRepository.GetCategoryWithArticle(id);
            return _mapper.Map<GetCategoryWithArticleDto>(entity);
        }


        public async Task<GetCatgoryWithBaseDto> GetCategoryWithBase(int id)
        {
            var entity = await _categoryRepository.GetCategoryWithBase(id);
            return _mapper.Map<GetCatgoryWithBaseDto>(entity);
        }
        public async Task<GetCategoryDto> UpdateCategory(AddCategoryDto addCategoryDto, int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (!string.IsNullOrEmpty(addCategoryDto.Name))
            {
                entity.Name = addCategoryDto.Name;
            }

            _genericRepository.Update(entity);
            _unitofWork.Commit();
            return _mapper.Map<GetCategoryDto>(entity);
        }
    }
}
