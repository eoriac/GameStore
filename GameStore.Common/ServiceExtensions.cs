using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.Common
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new()
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = configuration["Authentication:Issuer"],
                                    ValidAudience = configuration["Authentication:Audience"],
                                    IssuerSigningKey = new SymmetricSecurityKey(
                                        Encoding.ASCII.GetBytes(configuration["Authentication:SecretForKey"]))
                                };
                            });

            return services;
        }
    }
}
