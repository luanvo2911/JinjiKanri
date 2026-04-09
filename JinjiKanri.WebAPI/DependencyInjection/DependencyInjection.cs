using JinjiKanri.Domain.Repositories.Implements;
using JinjiKanri.Domain.Repositories.Interface;
using JinjiKanri.Domain.Services.Implements;
using JinjiKanri.Domain.Services.Interface;
using JinjiKanri.Entity.Entities;
using JinjiKanri.WebAPI.AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace JinjiKanri.WebAPI.DependencyInjection
{
    public class DependencyInjection
    {
        public WebApplicationBuilder InitBuilder(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            var JWTKey = builder.Configuration.GetSection("JsonWebToken:SecretKey").Value;

            if(JWTKey == null) { 
                throw new NullReferenceException("JWT Secret Key is not configured.");
            }

            var JWTByteKey = Encoding.ASCII.GetBytes(JWTKey);

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Configure Entity Framework Core with SQL Server
            builder.Services.AddDbContext<JinjiKanriContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register the Generic Base Repository
            builder.Services.AddScoped(typeof(IEmployeeRepository), typeof(EmployeeRepository));
            builder.Services.AddScoped(typeof(IVLoginRepository), typeof(VLoginRepository));

            // Register Specific Domain Services
            builder.Services.AddScoped(typeof(IEmployeeService), typeof(EmployeeService));
            builder.Services.AddScoped(typeof(IVLoginService), typeof(VLoginService));

            // Register Mapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            // Authentication and Authorization
            builder.Services.AddAuthentication("Bearer").AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(JWTByteKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });




            return builder;
        }
    }
}
