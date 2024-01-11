using ApplicationBusiness.Interfaces;
using Repository;
using Repository.UserRepository;

namespace ApplicationBusiness.UserManager
{
    public class UserManager : IUserManager
    {
        private readonly Context _context;
        private readonly ITokenManager _tokenManager;

        public UserManager(Context context, ITokenManager tokenManager)
        {
            _context = context;
            _tokenManager = tokenManager;
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
            if (userID != 0)
            {
                return _tokenManager.GenerateJSONWebToken(repository.GetById(userID));
            }
            else return null;

        }
    }
}
