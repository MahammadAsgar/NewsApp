using Microsoft.Extensions.DependencyInjection;
using NewsMedia.Persistance.JWT;


namespace NewsMedia.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}
