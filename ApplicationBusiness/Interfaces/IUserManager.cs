using System.Security.Claims;

namespace ApplicationBusiness.Interfaces
{
    public interface IUserManager
    {
        string Login(string username, string pass);
        bool Register(string username, string pass);
    }
}