using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplicationASP.NETCoreWebAPI.Models;
using WebApplicationASP.NETCoreWebAPI.Services;

namespace WebApplicationASP.NETCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointsOfInterestController : ControllerBase
    {
        // Dependency Injection
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            IMailService mailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }
        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            try
            {
               // throw new Exception("Exception example."); ==> Test throw exception to comment all code in try block
              var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");

                    return NotFound();
                }

                return Ok(city.PointOfInterests);
             
            }
            catch (Exception ex) {
                _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}", ex);
                // throw; //We can return statusCode instead of throw
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet("{id}", Name = "GetPointOfInterest")]
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

        [HttpPost]
        public IActionResult CreatePointOfInterest(int cityId,
            [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            /*if (pointOfInterest.Name == null)
            {
                return BadRequest();
            }*/ //Set the rule for model, then we can use the model to check the property

            if (pointOfInterest.Description == pointOfInterest.Name) //Add your own error
            {
                ModelState.AddModelError("Description", "The provide description should be different from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(
                c => c.PointOfInterests).Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };
            city.PointOfInterests.Add(finalPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest",
                new { cityId, id = finalPointOfInterest.Id }, finalPointOfInterest);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
            [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest.Description == pointOfInterest.Name) //Add your own error
            {
                ModelState.AddModelError("Description", "The provide description should be different from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(p => p.Id == id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;
            return NoContent(); //Can return Ok(pointOfInterest);
        }

        [HttpPatch("{id}")] // Partially Update
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(p => p.Id == id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
                {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
                };
            patchDoc.ApplyTo(pointOfInterestToPatch, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
            {
                ModelState.AddModelError(
                    "Description",
                    "The provide description should be different from the name.");
            }

            if (TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }
            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(p => p.Id == id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            city.PointOfInterests.Remove(pointOfInterestFromStore);
            _mailService.Send("Point of interest delete.", $"Point of interest {pointOfInterestFromStore.Name}" +
                $"with id {pointOfInterestFromStore.Id} was deleted.");
            return NoContent();
        }
    }
}
