using Microsoft.AspNetCore.Mvc;
using MovieRental.Movie;
using MovieRental.Rental;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {

        private readonly IRentalFeatures _features;

        public RentalController(IRentalFeatures features)
        {
            _features = features;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Rental.Rental rental)
        {

            var result = await _features.Save(rental);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetRentalsByCustomerName([FromQuery] string customerName)
        {
            return Ok(_features.GetRentalsByCustomerName(customerName));
        }
	}
}
