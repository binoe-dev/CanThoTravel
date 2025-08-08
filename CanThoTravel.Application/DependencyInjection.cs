using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CanThoTravel.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Configure AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Configure MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Configure FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}