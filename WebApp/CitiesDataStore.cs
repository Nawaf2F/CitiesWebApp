using WebApp.Models;

namespace WebApp
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore() {

            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Riyadh",
                    Description = "Capital",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Kingdom Tower",
                            Description = "A big tower"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Boulivard World",
                            Description = "A good entertaining experience"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Jeddah",
                    Description = "On the red Sea",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "King Fahad Fountain",
                            Description = "The biggest Fountain in the world"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 4,
                            Name = "Red sea mall",
                            Description = "Shopping place"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Dammam",
                    Description = "On the east",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "Dammam beach",
                            Description = "An amazing beach"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 4,
                    Name = "Hail",
                    Description = "On the north",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 6,
                            Name = "Aaerf Castle",
                            Description = "Old castle"
                        }
                    }
                },
            };
        }
    }
}
