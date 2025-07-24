
using CanThoTravel.Infrastructure.Configuration;
using CleanArc.Application.Repository;
using CleanArc.Application.Repository.PostgreSQL;
using CleanArc.Application.Service.Member;
using CleanArc.Infrastructure.Repositories;
using CleanArc.Infrastructure.Repository.Member;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace CleanArc.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IMemberRepository, MemberRepository>();
            builder.Services.AddScoped<IMemberService, MemberService>();

            var dbSettings = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
            if (dbSettings == null)
            {
                throw new InvalidOperationException("DatabaseSettings configuration section is missing or invalid.");
            }
            builder.Services.AddSingleton(dbSettings);

            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.AddScoped<NpgsqlConnection>(provider =>
            {
                var configuration = provider.GetRequiredService<DatabaseSettings>();
                
                return new Npgsql.NpgsqlConnection(configuration.PostgreConnectionString);
            });

            builder.Services.AddScoped<ITransactionManager, PostgreTransactionManager>(provider =>
            {
                var npgsqlConnection = provider.GetRequiredService<NpgsqlConnection>();
                if (npgsqlConnection == null)
                {
                    throw new InvalidOperationException("NpgsqlConnection is not declared.");
                }

                var configuration = provider.GetRequiredService<IConfiguration>();
                if (configuration == null)
                {
                    throw new InvalidOperationException("Configuration is not initialized.");
                }


                return new PostgreTransactionManager(npgsqlConnection, configuration);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
              
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
    