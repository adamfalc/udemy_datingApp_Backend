using API.Data;
using API.Data.Repositories;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddAppicationServices(this IServiceCollection services, IConfiguration config)
        {

            //datacontext
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            
            //cors
            services.AddCors();
            
            //services
            services.AddScoped<ITokenService, TokenService>();
            
            //repos
            services.AddScoped<IUserRepository, UserRepository>();
            
            //mapping
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

    }
}
