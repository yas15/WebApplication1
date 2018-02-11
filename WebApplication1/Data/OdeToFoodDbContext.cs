using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{

    // Each DbContext must inherit from the DbContext class.
    // A DbContext corresponds to a single database and 
    // a DbSet corresponds to a single table in a database.
    public class OdeToFoodDbContext : DbContext
    {
        // We are overriding the default constructor for the class by adding the
        // an optionial parameter of type DbContextOptions to the constructor.
        // DbContextOptions configuration information such as the database provider, connection string, etc.
        public OdeToFoodDbContext(DbContextOptions options) : base(options)
        {
        }

        // Here we are creating a new DbSet of type <Restaurant> class.
        // A DbSet corresponds to a table or view in your database.
        public DbSet<Restaurant> Restaurants { get; set; }
    }

}

