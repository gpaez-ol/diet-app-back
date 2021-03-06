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
                            .WithOrigins("*","http://possession-portion-process-depends.trycloudflare.com/")
                            .WithMethods(allowedMethods)
                            .WithHeaders(allowedHeaders);
                    });
            });

            return services;
        }
    }
}
