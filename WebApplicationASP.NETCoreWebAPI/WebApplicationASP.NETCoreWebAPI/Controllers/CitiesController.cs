using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationASP.NETCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCities()
        {
            var cities = new JsonResult(CitiesDataStore.Current.Cities);
            return Ok(cities);
        }

        /*  public JsonResult GetCities()
            {
                return new JsonResult(CitiesDataStore.Current.Cities);
                 return new JsonResult(
                     new List<object>()
                     {
                         new { id = 1, Name = "New York City"},
                         new { id = 2, Name = "Chicago"},
                           new { id = 3, Name = "Norman, OK"},
                         new { id = 4, Name = "Hawai"}
                     }); 
            } */


        [HttpGet("{id}")]
        /* public JsonResult GetCity(int id)
         {
             return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
         } */
        public IActionResult GetCity(int id) // Using IActionResult to return the statusCode
        {
            var cityToReturn = new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
            if (cityToReturn == null)
            {
                return NotFound();
            } 
            return Ok(cityToReturn);
        }
    }
}
