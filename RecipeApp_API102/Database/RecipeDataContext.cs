using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeAppAPI.Database
{
    public class RecipeDataContext : DbContext
    {
        public RecipeDataContext(DbContextOptions<RecipeDataContext> options) : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        public class YourDbContextFactory : IDesignTimeDbContextFactory<RecipeDataContext>
        {
            public RecipeDataContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<RecipeDataContext>();
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=RecipeBook;Integrated Security=True; TrustServerCertificate=True; Encrypt=false");

                return new RecipeDataContext(optionsBuilder.Options);
            }
        }
    }
    
}
