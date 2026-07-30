using E_Commerce.Application.Contracts;
using E_Commerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection ApplicationService (this IServiceCollection services)
        {
            services.AddAutoMapper(c => { }, typeof(ApplicationServiceRegistration).Assembly);
            services.AddScoped<IproductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddSingleton<ICachService, CacheService>();
            services.AddScoped<IAuthenticationService, AuhenticationService>();
            
            return services;
        }
    }
}
