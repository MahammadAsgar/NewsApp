using AutoMapper;
using NewsMedia.Domain.Models.Entities;
using NewsMedia.Domain.Models.Users;
using NewsMedia.Infrastructure.DTOS.Entities.Article.Get;
using NewsMedia.Infrastructure.DTOS.Entities.Article.Post;
using NewsMedia.Infrastructure.DTOS.Entities.ArticleFile;
using NewsMedia.Infrastructure.DTOS.Entities.Category.Get;
using NewsMedia.Infrastructure.DTOS.Entities.Category.Post;
using NewsMedia.Infrastructure.DTOS.Entities.CategoryBase.Get;
using NewsMedia.Infrastructure.DTOS.Entities.CategoryBase.Post;
using NewsMedia.Infrastructure.DTOS.Entities.Tag.Get;
using NewsMedia.Infrastructure.DTOS.Entities.Tag.Post;
using NewsMedia.Infrastructure.DTOS.Users;

namespace NewsMedia.Infrastructure.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region Entities
            CreateMap<Tag, GetTagDto>().ReverseMap();
            CreateMap<Tag, GetTagWithArticlesDto>().ReverseMap();
            CreateMap<Tag, AddTagDto>().ReverseMap();

            CreateMap<Category, GetCategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryWithArticleDto>().ReverseMap();
            CreateMap<Category, AddCategoryDto>().ReverseMap();

            CreateMap<CategoryBase, AddCategoryBaseDto>().ReverseMap();
            CreateMap<CategoryBase, GetCategoryBaseWithCategoriesDto>().ReverseMap();
            CreateMap<CategoryBase, GetCategoryBaseDto>().ReverseMap();

            CreateMap<Article, GetArticleDto>().ReverseMap();
            CreateMap<Article, GetArticleFullDto>().ReverseMap();
            CreateMap<AddArticleDto, Article>()
                .ForMember(dest => dest.ArticleContentFiles, opt => opt.Ignore())
                .ForMember(dest => dest.ArticleTitleFile, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore()).ReverseMap();

            CreateMap<ArticleTitleFile, GetFile>().ReverseMap();
            CreateMap<ArticleContentFile, GetFile>().ReverseMap();
            #endregion



            #region Users
            CreateMap<AppUser, SignUpDTO>()
            .ForMember(x => x.FullName, y => y.MapFrom(z => z.FullName))
            .ForMember(x => x.Email, y => y.MapFrom(z => z.Email));

            CreateMap<SignUpDTO, AppUser>()
            .ForMember(x => x.FullName, y => y.MapFrom(z => z.FullName))
            .ForMember(x => x.Email, y => y.MapFrom(z => z.Email));

            CreateMap<AppUser, SignInDto>().ReverseMap();

            CreateMap<AppUser, GetUserBasicData>().ReverseMap();
            #endregion
        }
    }
}
