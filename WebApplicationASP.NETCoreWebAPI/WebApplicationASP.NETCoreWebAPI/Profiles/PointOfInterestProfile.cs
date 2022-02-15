using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationASP.NETCoreWebAPI.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
        CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
        CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>().
            ReverseMap();
              //      CreateMap<Entities.PointOfInterest, Models.PointOfInterestForUpdateDto>();

    }
}
