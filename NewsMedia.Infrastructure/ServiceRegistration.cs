using Microsoft.Extensions.DependencyInjection;
using NewsMedia.Infrastructure.Mappings;
using NewsMedia.Infrastructure.Services.Entities.Abstractions;
using NewsMedia.Infrastructure.Services.Entities.Implementations;
using NewsMedia.Infrastructure.Services.Mail;
using NewsMedia.Infrastructure.Services.Storage;
using NewsMedia.Infrastructure.Services.Users.Abstractions;
using NewsMedia.Infrastructure.Services.Users.Implementations;

namespace NewsMedia.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryBaseService, CategoryBaseService>();
            services.AddScoped<IFilelStorage, FileStorage>();
            services.AddScoped<IMailService, MailService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddAutoMapper(cfg =>
            {
                //cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<MapProfile>();
            });
        }
    }
}
