using JinjiKanri.Domain.Repositories.Implements;
using JinjiKanri.Domain.Repositories.Interface;
using JinjiKanri.Domain.Services.Implements;
using JinjiKanri.Domain.Services.Interface;
using JinjiKanri.Entity.Entities;
using JinjiKanri.WebAPI.AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace JinjiKanri.WebAPI.DependencyInjection
{
    public class DependencyInjection
    {
        public WebApplicationBuilder InitBuilder(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Configure Entity Framework Core with SQL Server
            builder.Services.AddDbContext<JinjiKanriContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register the Generic Base Repository
            builder.Services.AddScoped(typeof(IEmployeeRepository), typeof(EmployeeRepository));

            // Register Specific Domain Services
            builder.Services.AddScoped(typeof(IEmployeeService), typeof(EmployeeService));

            // Register Mapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            return builder;
        }
    }
}
