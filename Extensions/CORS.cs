using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AlgoFit.Security.Extensions
{
    public static class CORS
    {
        public static IServiceCollection AddCorsPolicyAllowedHostWithCredentials(
            this IServiceCollection services,
            string[] origins, string[] allowedMethods, string[] allowedHeaders)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicyAllowedHostWithCredentials",
                    builder =>
                    {
                        builder
                            .WithOrigins(origins)
                            .WithMethods(allowedMethods)
                            .WithHeaders(allowedHeaders)
                            .AllowCredentials();
                    });
            });

            return services;
        }
    }
}
