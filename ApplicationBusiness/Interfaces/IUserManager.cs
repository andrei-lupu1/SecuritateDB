using DataTransformationObjects.Payloads;

namespace ApplicationBusiness.Interfaces
{
    public interface IUserManager
    {
        string Login(string username, string pass);
        int Register(string username, string pass);
        void CreatePerson(RegisterPayload registerPayload, int userID);
        int? GetRole(string token);
    }
}