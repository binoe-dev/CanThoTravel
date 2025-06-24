
using CanThoTravel.Infrastructure.Configuration;
using CleanArc.Application.Repository;
using CleanArc.Application.Service.Member;
using CleanArc.Infrastructure.Repository.Member;

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
            builder.Services.AddSingleton(dbSettings);

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
