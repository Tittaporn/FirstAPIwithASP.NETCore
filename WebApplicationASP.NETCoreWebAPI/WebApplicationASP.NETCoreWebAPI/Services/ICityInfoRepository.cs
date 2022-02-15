using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationASP.NETCoreWebAPI.Entities;

namespace WebApplicationASP.NETCoreWebAPI.Services
{
    public interface ICityInfoRepository
    {
        // IQueryable<City> GetCities()
        IEnumerable<City> GetCities();
        City GetCity(int cityId, bool includePointsOfInterest);
        IEnumerable<PointOfInterestForCreationDto> GetPointsOfInterestForCity(int cityId);
        PointOfInterestForCreationDto GetPointOfInterestForCity(int cityId, int pointOfInterestId);
        bool CityExists(int cityId);
        void AddPointOfInterestForCity(int cityId, PointOfInterestForCreationDto pointOfInterest);
        void UpdatePointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);
        void DeletePointOfInterest(PointOfInterest pointOfInterest);

        bool Save();
    }
}
