using ApplicationBusiness.Interfaces;
using ApplicationBusiness.UserManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Person;
using Models.Users;
using Repository;
using Repository.GenericRepository;
using Repository.UserRepository;
using SecuritateDBAPI.Models;

namespace SecuritateDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Context _context;
        private readonly IUserManager _userManager;

        public UsersController(Context context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var repository = new GenericRepository<Users>(_context);
            return Ok(new ApiResponse(true, "Lista utilizatori.", repository.GetAll()));
        }

        [Authorize]
        [HttpGet("GetAllPersons")]
        public IActionResult GetAllPersons()
        {
            var repository = new GenericRepository<Person>(_context);
            return Ok(new ApiResponse(true, "Lista persoane.", repository.GetIncluding(x => x.Role)));
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
                return Ok(new ApiResponse(true, "Utilizator logat.", _userManager.Login(username, pass)));
            }
            catch(Exception e)
            {
                return Ok(new ApiResponse(false, e.Message));
            }
        }

    }
}
