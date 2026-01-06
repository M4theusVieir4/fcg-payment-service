using FCG.Payments.Api._Common.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

namespace FCG.Payments.Api._Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFcgPaymentsApiSwagger(
        this IServiceCollection services,
        AuthenticationSettings authenticationSettings)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "FCG Payments API", Version = "v1" });

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Description = "Keycloak OAuth2 Authorization",
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri($"{authenticationSettings.Authority}/protocol/openid-connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { authenticationSettings.Audience, $"Access to {authenticationSettings.Audience}" }
                        }
                    }
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    },
                    new[] { authenticationSettings.Audience }
                }
            });
        });

        return services;
    }


    public static IServiceCollection ConfigureAuthentication(
        this IServiceCollection services,
        AuthenticationSettings authenticationSettings
    )
    {
        authenticationSettings.EnsureSettings();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options => {
            options.Authority = authenticationSettings.Authority;
            options.Audience = authenticationSettings.Audience;
            options.RequireHttpsMetadata = authenticationSettings.RequireHttpsMetadata;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }

    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(auth =>
        {
            auth.AddPolicy(Policies.OnlyAdmin, p => p
                .RequireClaim(ClaimTypes.Role, Roles.Admin));

            auth.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        return services;
    }

    private static void EnsureSettings(this AuthenticationSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        ArgumentException.ThrowIfNullOrWhiteSpace(settings.Authority);

        ArgumentException.ThrowIfNullOrWhiteSpace(settings.Audience);
    }
}
