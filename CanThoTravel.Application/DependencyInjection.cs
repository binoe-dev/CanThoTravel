using System.Reflection;
using CanThoTravel.Application.CQRS.Members.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace CanThoTravel.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register MediatR and AutoMapper for the application layer
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            return services;
        }
    }
}