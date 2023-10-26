using Microsoft.Extensions.DependencyInjection;
using RecipeAppAPI.Database;
using RecipeAppAPI.Migrations;

namespace Application.Web.Core.Configurations
{
    public static class CoreConfigurations
    {
        public static IServiceCollection ConfigureReadOnlyDbContext(this IServiceCollection services)
        {
            services.AddScoped<MigrationService>();

            return services;
        }
    }
}
