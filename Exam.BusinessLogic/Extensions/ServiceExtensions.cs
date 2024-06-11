using Exam.BusinessLogic.Services.Interfaces;
using Exam.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.BusinessLogic.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPersonService, PersonService>();

            return services;
        }
    }
}
