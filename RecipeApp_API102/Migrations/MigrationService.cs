using Microsoft.EntityFrameworkCore;
using RecipeAppAPI.Database;

namespace RecipeAppAPI.Migrations
{
    public class MigrationService
    {
        private readonly RecipeDataContext _context;

        public MigrationService(RecipeDataContext context)
        {
            _context = context;
        }

        public void ApplyMigrations()
        {
            // Apply pending migrations to the database
            _context.Database.Migrate();
        }
    }
}
