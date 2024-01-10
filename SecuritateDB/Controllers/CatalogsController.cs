using ApplicationBusiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecuritateDBAPI.Models;

namespace SecuritateDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogsController : ControllerBase
    {
        private readonly ICatalogsManager _catalogsManager;
        public CatalogsController(ICatalogsManager catalogsManager)
        {
            _catalogsManager = catalogsManager;
        }

        [Authorize]
        [HttpGet("GetCounties")]
        public IActionResult GetCounties()
        {
            var result = _catalogsManager.GetCounties();
            return Ok(new ApiResponse(true,string.Empty,result));
        }

        [Authorize]
        [HttpGet("GetCities")]
        public IActionResult GetCities(int countyID)
        {
            var result = _catalogsManager.GetCities(countyID);
            return Ok(new ApiResponse(true, string.Empty, result));
        }
    }
}
