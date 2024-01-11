using ApplicationBusiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecuritateDBAPI.Models;

namespace SecuritateDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet("Register")]
        public IActionResult Register(string username, string pass)
        {
            try
            {
                var result = _userManager.Register(username, pass);
                return Ok(new ApiResponse(result, "Utilizator inregistrat."));
            }
            catch(Exception e)
            {
                return Ok(new ApiResponse(false, e.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(string username, string pass)
        {
            try
            {
                var result = _userManager.Login(username, pass);
                return Ok(new ApiResponse(true, "Utilizator logat.", result));
            }
            catch(Exception e)
            {
                return Ok(new ApiResponse(false, e.Message));
            }
        }

    }
}
