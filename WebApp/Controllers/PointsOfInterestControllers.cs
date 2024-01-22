using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApp.Entities;
using WebApp.Models;
using WebApp.Services;
namespace WebApp.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    //[Authorize]
    [ApiController]
    public class PointsOfInterest: ControllerBase
    {
        private readonly CitiesDataStore _citiesDataStore;
        private readonly IMapper _mapper;
        private readonly ICityInfoRepository _cityInfoRepository;
        public PointsOfInterest(ICityInfoRepository cityInfoRepository, CitiesDataStore citiesDataStore, IMapper mapper) 
        { 

        _cityInfoRepository = cityInfoRepository ?? throw new ArgumentException(nameof(cityInfoRepository));
        _citiesDataStore = citiesDataStore ?? throw new ArgumentException(nameof(citiesDataStore));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId) 
        {
            // the city name of the user to authorize and check if it matches the requested ID
            var cityName = User.Claims.FirstOrDefault(c => c.Type == "city")?.Value;

            
            // Example: If the user is authenticated and does not have the same city as the city requested he will not be authorized
            // we have a duumy data city = riyadh
           // if(!await _cityInfoRepository.CityNameMatchesCityId(cityName, cityId))
           // {
             //   return Forbid();
            //}

          //find city
          if(!await _cityInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }

          var PointsOfInterestForCity = await _cityInfoRepository
                .GetPointsOfInterestForCityAsync(cityId);
            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(PointsOfInterestForCity));
        }

        [HttpGet("{pointofinterestsid}", Name = "GetPointOfInterest")]
        public async  Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointofinterestsid) 
        {
            //find city
            if (!await _cityInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterest = await _cityInfoRepository
                .GetPointOfInterestForCityAsync (cityId, pointofinterestsid);

            if(pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));

        }

        [HttpPost]
        public async  Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, PointOfInterestForCreationDto pointOfInterest)
        {
            //find city
            if (!await _cityInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }
            var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            await _cityInfoRepository.AddPointOfInterestForCityAsync(cityId, finalPointOfInterest);
            await _cityInfoRepository.SaveChangesAsync();

            var createdPointOfInterestTOReturn = _mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    PointOfInterestId = createdPointOfInterestTOReturn.Id,
                },
                createdPointOfInterestTOReturn);


        }

        [HttpPut("pointofinterestid")]
        public async  Task<ActionResult> UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
        {
            //find city
            if (!await _cityInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }

            // find point of interest
            var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            // update
            _mapper.Map(pointOfInterest, pointOfInterestEntity);
            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();

        }

        [HttpPatch("pointofinterestid")]
        public async Task<ActionResult> PartiallyUpdatePointOfInterest(int cityId, int pointofinterestid, JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            //find city
            if (!await _cityInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }

            // find point of interest
            var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointofinterestid);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = _mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);


            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);
            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("pointofinterestid")]
        public async Task<ActionResult> DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            //find city
            if (!await _cityInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }

            // find point of interest
            var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            //Delete
            _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);
            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

    }
}
