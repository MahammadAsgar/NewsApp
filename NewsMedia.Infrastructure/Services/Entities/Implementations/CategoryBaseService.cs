using AutoMapper;
using NewsMedia.Application.Repositories.Abstractions;
using NewsMedia.Application.UnitOfWorks;
using NewsMedia.Domain.Models.Entities;
using NewsMedia.Infrastructure.DTOS.Entities.CategoryBase.Get;
using NewsMedia.Infrastructure.DTOS.Entities.CategoryBase.Post;
using NewsMedia.Infrastructure.Services.Entities.Abstractions;

namespace NewsMedia.Infrastructure.Services.Entities.Implementations
{
    public class CategoryBaseService : ICategoryBaseService
    {
        readonly ICategoryBaseRepository _categoryBaseRepository;
        readonly IUnitofWork _unitofWork;
        readonly IMapper _mapper;
        readonly IGenericRepository<CategoryBase> _genericRepository;
        public CategoryBaseService(ICategoryBaseRepository categoryBaseRepository, IUnitofWork unitofWork, IMapper mapper, IGenericRepository<CategoryBase> genericRepository)
        {
            _categoryBaseRepository = categoryBaseRepository;
            _unitofWork = unitofWork;
            _mapper = mapper;
            _genericRepository = genericRepository;
        }
        public async Task AddCategoryBase(AddCategoryBaseDto addCategoryBaseDto)
        {
            var entity = _mapper.Map<CategoryBase>(addCategoryBaseDto);
            await _genericRepository.AddAsync(entity);
            await _unitofWork.CommitAsync();
        }

        public Task DeleteCategoryBase(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetCategoryBaseWithCategoriesDto> GetCategoryBase(int id)
        {
            var entity = await _categoryBaseRepository.GetCategoryBaseWithCategory(id);
            return _mapper.Map<GetCategoryBaseWithCategoriesDto>(entity);
        }

        public async Task<IEnumerable<GetCategoryBaseWithCategoriesDto>> GetCategoryBasesWithCategories()
        {
            var entities = await _categoryBaseRepository.GetCategoryBaseWithCategories();
            return _mapper.Map<IEnumerable<GetCategoryBaseWithCategoriesDto>>(entities);
        }

        public async Task<IEnumerable<GetCategoryBaseDto>> GetCategoryBases()
        {
            var entities = await _categoryBaseRepository.GetCategoryBaseWithCategories();
            return _mapper.Map<IEnumerable<GetCategoryBaseDto>>(entities);
        }

        public Task<GetCategoryBaseDto> UpdateCategoryBase(AddCategoryBaseDto addCategoryBaseDto, int id)
        {
            throw new NotImplementedException();
        }
    }
}
