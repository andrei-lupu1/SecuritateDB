using ApplicationBusiness.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.UserRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApplicationBusiness.UserManager
{
    public class UserManager : IUserManager
    {
        private readonly Context _context;
        private IConfiguration _config;
        public UserManager(Context context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }

        public bool Register(string username, string pass)
        {
            var repository = new UserRepository(_context);
            return repository.Register(username, pass) != 0;
        }

        public string Login(string username, string pass)
        {
            var repository = new UserRepository(_context);
            var userID = repository.Login(username, pass);
            if(userID != 0)
            {
                return GenerateJSONWebToken(userID, repository.GetById(userID).USERNAME);
            }
            else return null;

        }
        private string GenerateJSONWebToken(int userID, string Username)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim("ID", userID.ToString()),
            new Claim("Username", Username)
        };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
