using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StorEsc.Api.Token.Interfaces;
using StorEsc.Api.Token.Services;

namespace StorEsc.Api.IoC;

public static class TokenDependencies
{
    public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var secretKey = configuration["Token:SecretKey"];
        var issuer = configuration["Token:Issuer"];

        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidIssuer = issuer
                };
            });

        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}