using ApplicationBusiness.Interfaces;
using DataTransformationObjects.Payloads;
using Models.Person;
using Repository;
using Repository.GenericRepository;
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

        public int Register(string username, string pass)
        {
            var repository = new UserRepository(_context);
            return repository.Register(username, pass);
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

        public void CreatePerson(RegisterPayload registerPayload, int userID)
        {
            var personRepository = new GenericRepository<Person>(_context);
            var person = new Person
            {
                NUME = registerPayload.Name,
                TELEFON = registerPayload.PhoneNumber,
                EMAIL = registerPayload.Email,
                USER_ID = userID,
                ROLE_ID = 3
            };
            _context.BeginTransaction();
            personRepository.Add(person);
            _context.SaveChanges();
            _context.CommitTransaction();
        }
    }
}
