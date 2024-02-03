using Microsoft.EntityFrameworkCore;
using WebApp.Entities;

namespace WebApp.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> cities { get; set; } = null!;
        public DbSet<PointOfInterest> pointOfInterests { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options) 
        { 
        }
    }
}
