using ApiProjectPractise.Data;
using ApiProjectPractise.Profiles;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ApiProjectPractise
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(opt=>opt.AddProfile(new MapperProfile(new HttpContextAccessor())));
            services.AddValidatorsFromAssemblyContaining<Program>();
        }
    }
}
