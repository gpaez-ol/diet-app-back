using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AlgoFit.Data.Context;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AlgoFit.Repositories.Manager;
using AlgoFit.WebAPI.Logic;
using AutoMapper;
using AlgoFit.Errors.Managers;
namespace AlgoFit
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AlgoFit", Version = "v1" });
            });
              services.AddDbContext<AlgoFitContext>(
                 opt => ConfigureDatabaseService(opt),
                 ServiceLifetime.Scoped
             );
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<RepositoryManager>();
            services.AddScoped<SessionLogic>();
            services.AddScoped<UserLogic>();
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
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler(err => ExceptionHandler.UseAlgoFitExceptionHandler(err));
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AlgoFit v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                //  var context = serviceScope.ServiceProvider.GetRequiredService<AlgoFitContext>();
                //  context.Database.EnsureCreated();
                // TODO: Add Data Init DataInit.Init(context);
            }
        }
    }
}
