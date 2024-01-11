using ApplicationBusiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Models.Catalogs;
using Repository.GenericRepository;
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

        [Authorize]
        [HttpGet("GetOrdersForCourier")]
        public IActionResult GetOrdersForCourier()
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Split("Bearer ")[1];
            if (token is not null)
            {
                try
                {
                    var orders = _courierManager.GetOrdersForCourier(token);
                    return Ok(new ApiResponse(true, "Lista comenzilor pentru curier.", orders));
                }
                catch (Exception e)
                {
                    return Ok(new ApiResponse(false, e.Message));
                }
            }
            else return Ok(new ApiResponse(false, "Nu aveti acces la aceasta informatie."));
        }

        [Authorize]
        [HttpGet("CourierStartWorking")]
        public IActionResult CourierStartWorking(int vehicleID)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Split("Bearer ")[1];
            if(token is not null)
            {
                try
                {
                    _courierManager.CourierStartWorking(token, vehicleID);
                    return Ok(new ApiResponse(true, "Pontajul de inceput al zilei a fost realizat cu succes."));
                }
                catch(Microsoft.EntityFrameworkCore.DbUpdateException e)
                {
                    var message = string.Empty;
                    if(e.InnerException is not null)
                    {
                        message = e.InnerException.Message.Split("\n")[0];
                    }
                    return Ok(new ApiResponse(false, message));
                }
                catch (Exception e)
                {
                    return Ok(new ApiResponse(false, e.Message));
                }
            }
            else return Ok(new ApiResponse(false, "Nu aveti acces la aceasta informatie."));
        }

        [Authorize]
        [HttpGet("MarkOrderAsDone")]
        public IActionResult MarkOrderAsDone(int orderID)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Split("Bearer ")[1];
            if (token is not null)
            {
                try
                {
                    _courierManager.MarkOrderAsDone(token, orderID);
                    return Ok(new ApiResponse(true, "Comanda a fost finalizata cu succes."));
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
