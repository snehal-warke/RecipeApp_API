using Application.Web.Core.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RecipeAppAPI.Database;
using RecipeAppAPI.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RecipeAppAPI.Database.RecipeDataContext;

namespace RecipeAppAPI
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
            //var connectionString = "Server=localhost\\SQLEXPRESS;Initial Catalog=RecipeBook;Integrated Security=True; TrustServerCertificate=True; Encrypt=false";
            services.AddDbContext<RecipeDataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("YourDatabaseConnection")));

            services.ConfigureReadOnlyDbContext();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            services.AddSwaggerGen();

            var serviceProvider = services.BuildServiceProvider();
            var migrationService = serviceProvider.GetRequiredService<MigrationService>();
            migrationService.ApplyMigrations();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAngularApp");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Recipe App V1");
            });
        }
    }
}
