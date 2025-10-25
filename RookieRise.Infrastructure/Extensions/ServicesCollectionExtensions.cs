using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RookieRise.Application.Repositories;
using RookieRise.Application.Services;
using RookieRise.Infrastructure.Repositories;
using RookieRise.Infrastructure.Services;

namespace RookieRise.Infrastructure.Extensions
{
    public static class ServicesCollectionExtensions
    {
        
        
      
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // ✅ JWT Authentication
            var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            services.AddAuthorization();

            // ✅ Register your custom services/repositoriesservices.AddScoped<IGenerateJwtToken, GenerateJwtToken>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IOtpRepository, OtpRepository>();
            services.AddScoped<IEmailLogsRepository, EmailLogsRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IOtpService, OTPService>();
            services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IGenerateJwtToken, GenerateJwtToken>();

            services.AddHttpClient();

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // ✅ AutoMapper + MediatR
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            return services;
        }
    }
}


      
