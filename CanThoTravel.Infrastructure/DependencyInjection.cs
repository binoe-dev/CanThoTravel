using CanThoTravel.Application.IRepositories.Authentication;
using CanThoTravel.Application.IRepositories.Food;
using CanThoTravel.Application.Repository;
using CanThoTravel.Application.Repository.PostgreSQL;
using CanThoTravel.Infrastructure.Configuration;
using CanThoTravel.Infrastructure.Repositories;
using CanThoTravel.Infrastructure.Repositories.Food;
using CanThoTravel.Infrastructure.Repository.Member;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace CanThoTravel.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Add configuration for DatabaseSettings
            var dbSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
            if (dbSettings == null)
            {
                throw new InvalidOperationException("DatabaseSettings configuration section is missing or invalid.");
            }
            services.AddSingleton(dbSettings);

            // Register NpgsqlConnection and PostgreTransactionManager
            services.AddScoped(_ => new NpgsqlConnection(dbSettings.PostgreConnectionString));
            services.AddScoped<ITransactionManager, PostgreTransactionManager>();

            // Register Repository
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<ITokenGenerator, AuthManager>();
            services.AddScoped<IPasswordHasher, AuthManager>();

            return services;
        }
    }
}