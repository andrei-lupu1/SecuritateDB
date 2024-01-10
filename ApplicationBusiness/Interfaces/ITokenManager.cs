using Models.Users;
using System.Security.Claims;

namespace ApplicationBusiness.Interfaces
{
    public interface ITokenManager
    {
        IEnumerable<Claim> ExtractClaims(string jwtToken);
        string GenerateJSONWebToken(User user);
    }
}