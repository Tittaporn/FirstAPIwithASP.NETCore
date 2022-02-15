using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplicationASP.NETCoreWebAPI.Models;
using WebApplicationASP.NETCoreWebAPI.Services;

namespace WebApplicationASP.NETCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        private object cityEntities;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ??
                    throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
      public IActionResult GetCities()
        {
            //var cities = new JsonResult(CitiesDataStore.Current.Cities);
            //return Ok(cities);
            /*  var cityEntities = _cityInfoRepository.GetCities();
              var results = new List<CityWithoutPointsOfInterestDto>();
              foreach (var cityEntity in cityEntities)
              {
                  results.Add(new CityWithoutPointsOfInterestDto
                  {
                      Id = cityEntity.Id,
                      Description = cityEntity.Description,
                      Name = cityEntity.Name

                  }) ; 
              }
              return Ok(results);
            */
            // Using Mapper Instead Foreach
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));
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
        public IActionResult GetCity(int id, bool includePointsOfInterest = false) // Using IActionResult to return the statusCode
        {
            /*     var cityToReturn = new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
                 if (cityToReturn == null)
                 {
                     return NotFound();
                 } 
                 return Ok(cityToReturn); */
            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);
            if (city == null)
            {
                return NotFound();
            }
            if (includePointsOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
                //  var cityResult = _mapper.Map<CityDto>(city);
                /*   var cityResult = new CityDto()
                   {
                       Id = city.Id,
                       Name = city.Name,
                       Description = city.Description
                   };

                   foreach (var poi in city.PointOfInterests)
                   {
                       cityResult.PointOfInterests.Add(
                           new PointOfInterestDto()
                           {
                               Id = poi.Id,
                               Name = poi.Name,
                               Description = poi.Description
                           });
                   }
                   return Ok(cityResult);*/
            }
            var cityWithoutPointsOfInterestResult =
                new CityWithoutPointsOfInterestDto()
                {
                    Id = city.Id,
                    Description = city.Description,
                    Name = city.Name
                };
            //  return Ok(cityWithoutPointsOfInterestResult);
            return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(city));
        }
    }
}
