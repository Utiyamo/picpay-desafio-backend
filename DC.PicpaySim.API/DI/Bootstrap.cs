using DC.PicpaySim.Infrastructure.ORM;
using DC.PicpaySim.Infrastructure.Repositories;
using DC.PicpaySim.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DC.PicpaySim.API.DI
{
    public static class Bootstrap
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();

            services.AddDbContext<DatabaseContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("Default"))
           );

            AddRepositories(services);
            AddServices(services);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void AddServices(IServiceCollection services)
        {

        }

    }
}
