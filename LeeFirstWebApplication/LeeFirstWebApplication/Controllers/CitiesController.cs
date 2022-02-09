using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplicationASP.NETCoreWebAPI.Models;

namespace LeeFirstWebApplication.Controllers
{
    [ApiController]
    [Route("api/cities")]
    //  [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        //  [HttpGet("api/cities")]
      [HttpGet]
        public JsonResult GetCities()
        {
            return new JsonResult(CitiesDataStore.Current.Cities);

        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
    }
}
