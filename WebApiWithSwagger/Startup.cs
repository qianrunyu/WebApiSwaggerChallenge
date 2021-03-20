using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiWithSwagger.BusinessLogic;
using WebApiWithSwagger.EFCore;
using WebApiWithSwagger.Validation;

namespace WebApiWithSwagger
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddLogging();
            // Register Swagger
            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Carsales API", Version = "version 1" });
           });
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("Gary_CarSales"));

            services.AddSingleton<ICarManagement, CarManagement>();
            services.AddSingleton<IValidation, MyValidation>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }
    }
}