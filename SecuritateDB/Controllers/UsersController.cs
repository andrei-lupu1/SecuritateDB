using ApplicationBusiness.Interfaces;
using ApplicationBusiness.UserManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Users;
using Repository;
using Repository.GenericRepository;
using Repository.UserRepository;

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
            return this.Ok(repository.GetAll());
        }

        [HttpGet("Register")]
        public IActionResult Register(string username, string pass)
        {
            return Ok(_userManager.Register(username, pass));
        }

        [HttpPost("Login")]
        public IActionResult Login(string username, string pass)
        {
            return Ok(_userManager.Login(username, pass));
        }

    }
}
