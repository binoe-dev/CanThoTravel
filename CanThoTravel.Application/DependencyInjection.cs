using CanThoTravel.Application.CQRS.Members.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace CanThoTravel.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllMembersQuery>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetByIDMembersQuery>());

            return services;
        }
    }
}