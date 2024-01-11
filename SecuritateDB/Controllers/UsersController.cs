using ApplicationBusiness.Interfaces;
using DataTransformationObjects.Payloads;
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
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterPayload registerPayload)
        {
            try
            {
                var userID = _userManager.Register(registerPayload.Username, registerPayload.Password);
                _userManager.CreatePerson(registerPayload, userID);
                return Ok(new ApiResponse(userID != 0, "Utilizator inregistrat."));
            }   
            catch(Exception e)
            {
                return Ok(new ApiResponse(false, e.Message));
            }
        }

        [AllowAnonymous]
        [HttpGet("Login")]
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
