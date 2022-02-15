using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationASP.NETCoreWebAPI.Contexts;
using WebApplicationASP.NETCoreWebAPI.Entities;

namespace WebApplicationASP.NETCoreWebAPI.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IEnumerable<City> GetCities()
        {
            //   throw new NotImplementedException();
            return _context.Cities.OrderBy(c => c.Name).ToList();
        }

        public City GetCity(int cityId, bool includePointsOfInterest)
        {
            //   throw new NotImplementedException();
            if (includePointsOfInterest)
            {
                return _context.Cities.Include(c => c.PointsOfInterest)
                        .Where(c => c.Id == cityId).FirstOrDefault();

            }
            return _context.Cities
                .Where(c => c.Id == cityId).FirstOrDefault();
        }


        public PointOfInterestForCreationDto GetPointOfInterestForCity(int cityId, int pointOfInterestId)
        {
            //  throw new NotImplementedException();
            return _context.PointsOfInterest.Where(p => p.CityId == cityId && p.Id == pointOfInterestId).FirstOrDefault();
        }

        public IEnumerable<PointOfInterestForCreationDto> GetPointsOfInterestForCity(int cityId)
        {
            //throw new NotImplementedException();
            return _context.PointsOfInterest
                .Where(p => p.CityId == cityId).ToList();
        }

        public bool CityExists(int cityId)
        {
            return _context.Cities.Any(c => c.Id == cityId);
        }

        public void AddPointOfInterestForCity(int cityId, PointOfInterestForCreationDto pointOfInterest)
        {
            var city = GetCity(cityId, false);
            city.PointOfInterests.Add(pointOfInterest);
           // throw new NotImplementedException();
        }

        public void UpdatePointOfInterestForCity(int cityId, PointOfInterest pointOfInterest)
        {

        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
           // throw new NotImplementedException();
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointsOfInterest.Remove(pointOfInterest);
        }
    }
}
