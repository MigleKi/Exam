using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Exam.Database.Repositories.Interfaces;
using Exam.Database.Repositories;

namespace Exam.Database.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddDbContext<MainDbContext>(options =>
            {

                options.UseSqlServer(connectionString);

            });
            return services;
        }
    }
}