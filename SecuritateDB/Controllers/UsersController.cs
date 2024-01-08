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

        public UsersController(Context context)
        {
            _context = context;
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var repository = new GenericRepository<Users>(_context);
            return this.Ok(repository.GetAll());
        }

        [HttpGet("Register")]
        public IActionResult Register(string username, string pass)
        {
            var repository = new UserRepository(_context);
            return Ok(repository.Register(username, pass));
        }

    }
}
