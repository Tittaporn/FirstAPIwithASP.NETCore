using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationASP.NETCoreWebAPI.Models;

namespace WebApplicationASP.NETCoreWebAPI
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }
        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
        {
            new CityDto()
            {
            Id = 1,
            Name = "New York City",
            Description = "The one with that big park.",
            PointOfInterests = new List<PointOfInterestDto>()
            {
                new PointOfInterestDto() {
                Id = 1,
                Name = "Central Park",
                Description = "The most visited urban park in the United State."
                },
                new PointOfInterestDto() {
                Id = 2,
                Name = "Empire State Building",
                Description = "A 102-story skyscraper located in Midtown Manhattan."
                }
            }
            },
            new CityDto()
            {
            Id = 2,
            Name = "Chicago",
            Description = "The one I loved.",
              PointOfInterests = new List<PointOfInterestDto>()
            {
                new PointOfInterestDto() {
                Id = 1,
                Name = "Lake Shore Drive",
                Description = "The most beutiful shore in town."
                },
                new PointOfInterestDto() {
                Id = 2,
                Name = "Evanton",
                Description = "My dream houses."
                },
                 new PointOfInterestDto() {
                Id = 3,
                Name = "The Bean",
                Description = "The biggest bean in the world."
                }
            }
            },
            new CityDto()
            {
            Id = 3,
            Name = "Hululala",
            Description = "The one I wish to live.",
              PointOfInterests = new List<PointOfInterestDto>()
            {
                new PointOfInterestDto() {
                Id = 1,
                Name = "Gyukaku",
                Description = "Best food ever."
                },
                new PointOfInterestDto() {
                Id = 2,
                Name = "American Cruise",
                Description = "Once in the life time."
                }
            }
            }
        };
        }
    }
}
