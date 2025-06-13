using RedBook.Core.Security;
using RedBook.Core.IoC;
using Data.Entities;

namespace WebAPI.Configurations
{
    public static class DependencyResolver
    {
        public static void RosolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // DB Context & Other relevant mappings for Blume Core Library
            CoreDependencyResolver<AppDBContext>.RosolveCoreDependencies(services, configuration);

            // Security context mapping
            services.AddScoped<IClaimsPrincipalAccessor, HttpContextClaimsPrincipalAccessor>();

            // Services
            //services.AddScoped<IProductService, ProductService>();
        }
    }
}
