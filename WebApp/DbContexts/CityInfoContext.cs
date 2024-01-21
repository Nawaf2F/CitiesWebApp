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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("Riyadh")
                {
                    Id = 1,
                    Description = "Capital"
                },
                new City("Jeddah")
                {
                    Id = 2,
                    Description = "On the red Sea"
                },
                new City("Dammam")
                {
                    Id = 3,
                    Description = "On the east"
                },
                new City("Hail")
                {
                    Id = 4,
                    Description = "On the north"
                });
            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("Kingdom Tower")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "A big tower"

                },
                new PointOfInterest("Boulivard World")
                {
                    Id = 2,
                    CityId = 1,
                    Description = "A good entertaining experience"

                },
                new PointOfInterest("King Fahad Fountain")
                {
                    Id = 3,
                    CityId = 2,
                    Description = "The biggest Fountain in the world"

                },
                new PointOfInterest("Red sea mall")
                {
                    Id = 4,
                    CityId = 2,
                    Description = "Shopping place"

                },
                new PointOfInterest("Dammam beach")
                {
                    Id = 5,
                    CityId = 3,
                    Description = "An amazing beach"

                },
                new PointOfInterest("Aaerf Castle")
                {
                    Id = 6,
                    CityId = 4,
                    Description = "Old castle"

                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
