using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class Cities: ControllerBase
    {
        [HttpGet]
        public JsonResult GetCities()
        {

            return new JsonResult(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id) {

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            
            if(city == null)
            {
                return NotFound();
            }
            return Ok(city);
           
        }

    }
}
