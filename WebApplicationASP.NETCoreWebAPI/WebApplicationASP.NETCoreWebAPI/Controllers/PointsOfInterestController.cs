using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationASP.NETCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }
            return Ok(city.PointOfInterests);
        }

        [HttpGet("{id}")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            // Find City
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            // Find Point Of Interest
            var pointOfInterest = city.PointOfInterests.FirstOrDefault(p => p.Id == id);
            if (pointOfInterest == null)
            {
                return NotFound();
            } 
            return Ok(pointOfInterest);
        }
    }
}
