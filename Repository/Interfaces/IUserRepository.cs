using Models.Users;

namespace Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        int Register(string username, string password);

        int Login(string username, string password);

    }
}
