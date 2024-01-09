using ApplicationBusiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SecuritateDBAPI.Models;

namespace SecuritateDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourierController : ControllerBase
    {
        private readonly ICourierManager _courierManager;

        public CourierController(ICourierManager courierManager)
        {
            _courierManager = courierManager;
        }

        [Authorize]
        [HttpGet("GetAvailableVehicles")]
        public IActionResult GetAvailableVehicles()
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Split("Bearer ")[1];
            if (token != null)
            {
                try
                {
                    var availableVehicles = _courierManager.GetAvailableVehicles(token);
                    return Ok(new ApiResponse(true, "Lista masini disponibile.", availableVehicles));
                }
                catch (Exception e)
                {
                    return Ok(new ApiResponse(false, e.Message));
                }
            }
            else return Ok(new ApiResponse(false, "Nu aveti acces la aceasta informatie."));

        }
    }
}
