using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
namespace WebApp.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterest: ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId) 
        { 
            // find city
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }
            return Ok(city.PointOfInterest);
        
        }

        [HttpGet("{pointofinterestsid}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointofinterestsid) 
        {
            // find city
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            // find point of interest
            var POI = city.PointOfInterest.FirstOrDefault(c => c.Id == pointofinterestsid);

            if (POI == null)
            {
                return NotFound();
            }
            return Ok(POI);
        }

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId, PointOfInterestForCreationDto pointOfInterest) {

            // find city
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            // to store the id automatically
            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(
                c => c.PointOfInterest).Max(p => p.Id);

            // a new object to post in the DB
            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    PointOfInterestId = finalPointOfInterest.Id,
                },
                finalPointOfInterest);


        }

        [HttpPut("pointofinterestid")]
        public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
        {
            // find city
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            // find point of interest
            var POI = city.PointOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

            if (POI == null)
            {
                return NotFound();
            }

            // update
            POI.Name = pointOfInterest.Name;
            POI.Description = pointOfInterest.Description;

            return NoContent();

        }

        [HttpPatch("pointofinterestid")]
        public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointofinterestid, JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            // find city
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            // find point of interest
            var POI = city.PointOfInterest.FirstOrDefault(c => c.Id == pointofinterestid);

            if (POI == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = POI.Name,
                Description = POI.Description,
            };

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }
            POI.Name = pointOfInterestToPatch.Name;
            POI.Description =pointOfInterestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("pointofinterestid")]
        public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            // find city
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            // find point of interest
            var POI = city.PointOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

            if (POI == null)
            {
                return NotFound();
            }

            //Delete
            city.PointOfInterest.Remove(POI);
            return NoContent();
        }

    }
}
