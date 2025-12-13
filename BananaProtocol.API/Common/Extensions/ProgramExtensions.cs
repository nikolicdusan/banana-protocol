using System.Security.Claims;
using System.Text;
using BananaProtocol.API.Common.Authorization;
using BananaProtocol.Contracts.Enums;
using BananaProtocol.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BananaProtocol.API.Common.Extensions;

public static class ProgramExtensions
{
    public static AuthenticationBuilder ConfigureAuthentication(this IServiceCollection services, JwtOptions jwtOptions) =>
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey));
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = issuerSigningKey
                };
            });

    public static AuthorizationBuilder ConfigureAuthorization(this IServiceCollection services) =>
        services
            .AddAuthorizationBuilder()
            .AddDefaultPolicy(PolicyName.RequireAuthenticatedUser, policy =>
                policy.RequireAuthenticatedUser())
            .AddPolicy(PolicyName.RequireAdmin, policy =>
                policy.RequireClaim(ClaimTypes.Role, nameof(RoleType.Admin)));

    public static IServiceCollection ConfigureSwagger(this IServiceCollection services) =>
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Banana Protocol API", Version = "v1" });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter JWT token starting with **Bearer** and a space. Example: `Bearer eyJhbGciOi...`"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
}