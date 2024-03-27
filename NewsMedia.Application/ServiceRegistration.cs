using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsMedia.Application.Config;
using NewsMedia.Application.Context;
using NewsMedia.Application.Repositories.Abstractions;
using NewsMedia.Application.Repositories.Implementations;
using NewsMedia.Application.UnitOfWorks;
using NewsMedia.Domain.Models.Users;

namespace NewsMedia.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddDbContext<NewsDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<NewsDbContext>()
            .AddDefaultTokenProviders();


            services.AddScoped<IUnitofWork, UnitofWork>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryBaseRepository, CategoryBaseRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
