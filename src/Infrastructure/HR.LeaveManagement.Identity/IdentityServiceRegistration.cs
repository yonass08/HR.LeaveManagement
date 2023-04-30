using System.Text;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using HR.LeaveManagement.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HR.LeaveManagement.Identity;

public static class IdentityServiceRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services,
                                                                  IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddDbContext<LeaveManagementIdentityDbContext>(options =>
        options.UseNpgsql(
            configuration.GetConnectionString("LeaveManagementIdentityConnectionString"
            ), 
        options => options.MigrationsAssembly(typeof(LeaveManagementIdentityDbContext).Assembly.FullName)));

        services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<LeaveManagementIdentityDbContext>()
        .AddDefaultTokenProviders();

        services.AddTransient<IAuthService, AuthService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
            };
        });
        return services;
    }
}