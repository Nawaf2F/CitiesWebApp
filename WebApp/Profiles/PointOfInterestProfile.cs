using AutoMapper;

namespace WebApp.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile() {

        //Get    
        CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();

        //Create
        CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();

        //Update
        CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>();

        //Patch
        CreateMap<Entities.PointOfInterest, Models.PointOfInterestForUpdateDto>();
        }
    }
}
