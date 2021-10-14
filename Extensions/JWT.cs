using System.Text;
using System.Threading.Tasks;
using AlgoFit.BaseEntities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AlgoFit.Security.Extensions
{
    public static class JWT
    {
        public static IServiceCollection AddJwtWithProtectedCookie(
            this IServiceCollection services,
            TokenOptions tokenOptions,
            CookieOptions cookieOptions)
        {
            services.AddAuthentication(configureOptions =>
            {
                configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Key))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["Token"];
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        Utils.TokenHandler.RemoveTokenToCookie(context.Response);
                        return Task.CompletedTask;
                    }
                };
            });

            Utils.TokenHandler.SetTokenOptions(tokenOptions);
            Utils.TokenHandler.SetCookieOptions(cookieOptions);

            return services;
        }
    }
}
