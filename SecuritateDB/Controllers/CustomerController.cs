using ApplicationBusiness.Interfaces;
using DataTransformationObjects.Payloads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SecuritateDBAPI.Models;

namespace SecuritateDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;
        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        [Authorize]
        [HttpPost("AddOrder")]
        public IActionResult AddOrder([FromBody]OrderPayload orderPayload)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Split("Bearer ")[1];
            if (token != null)
            {
                try
                {
                    var order = _customerManager.AddOrder(token,orderPayload);
                    return Ok(new ApiResponse(true, "Comanda adaugata cu succes.", order));
                }
                catch (Exception e)
                {
                    return Ok(new ApiResponse(false, e.Message));
                }
            }
            else return Ok(new ApiResponse(false, "Nu aveti acces la aceasta informatie."));
        }

        [Authorize]
        [HttpGet("GetOrdersForCustomer")]
        public IActionResult GetOrdersForCustomer()
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Split("Bearer ")[1];
            if (token != null)
            {
                try
                {
                    var orders = _customerManager.GetOrdersForCustomer(token);
                    return Ok(new ApiResponse(true, "Lista comenzilor pentru client.", orders));
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
