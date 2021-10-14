using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using AlgoFit.Data.Context;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AlgoFit.Repositories.Manager;
using AlgoFit.WebAPI.Logic;
using AutoMapper;
using AlgoFit.Errors.Managers;
using AlgoFit.Security.Utils;
using AlgoFit.Security.Extensions;
using AlgoFit.BaseEntities;
using NFact.WebAPI.Logic;

namespace AlgoFit
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private CookieOptions CookieSecurityOptions { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var tokenOptions = new TokenOptions(Configuration["Jwt:Issuer"],Configuration["Jwt:Key"],Configuration.GetValue<double>("Jwt:Expiration"));
            var cookieOptions = new CookieOptions{
                Domain = Configuration["Cookie:Domain"],
                SameSite = Configuration.GetValue<SameSiteMode>("Cookie:SameSite"),
                HttpOnly = Configuration.GetValue<bool>("Cookie:HttpOnly"),
                Secure = Configuration.GetValue<bool>("Cookie:Secure")
            };
            services.AddControllers();
            services.AddJwtWithProtectedCookie
                (
                    Authorization.CreateTokenOptions(tokenOptions),
                    Authorization.CreateCookieOptions(cookieOptions)
                );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AlgoFit", Version = "v1" });
            });
            services.AddCorsPolicyAllowedHostWithCredentials(
                Configuration.GetSection("AllowedHosts").Get<string[]>(),
                new string[] { "GET", "PUT", "POST", "DELETE" },
                new string[] { "Content-Type", "Content-Length" }
            );
              services.AddDbContext<AlgoFitContext>(
                 opt => ConfigureDatabaseService(opt),
                 ServiceLifetime.Scoped
             );
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<RepositoryManager>();
            services.AddScoped<SessionLogic>();
            services.AddScoped<UserLogic>();
            services.AddScoped<IngredientLogic>();
            services.AddScoped<MealLogic>();
            services.AddScoped<DietLogic>();
        }

        public void ConfigureDatabaseService(DbContextOptionsBuilder optionsAction)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            if (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")))
                connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            optionsAction.UseMySql(connectionString,mySqlOptions =>
            {
                mySqlOptions.ServerVersion(new Version(5, 7), ServerType.MySql);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(err => ExceptionHandler.UseAlgoFitExceptionHandler(err));
            app.UseCors("CorsPolicyAllowedHostWithCredentials");
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AlgoFit v1"));
        
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                 var context = serviceScope.ServiceProvider.GetRequiredService<AlgoFitContext>();
                 context.Database.EnsureCreated();
                // TODO: Add Data Init DataInit.Init(context);
            }
        }
    }
}
