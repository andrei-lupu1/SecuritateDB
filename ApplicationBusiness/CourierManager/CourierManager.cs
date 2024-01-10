using ApplicationBusiness.Interfaces;
using Models.Catalogs;
using Models.Enums;
using Models.Person;
using Models.Vehicles;
using Repository;
using Repository.GenericRepository;

namespace ApplicationBusiness.CourierManager
{
    public class CourierManager : ICourierManager
    {
        private readonly Context _context;
        private readonly ITokenManager _tokenManager;

        public CourierManager(Context context, ITokenManager tokenManager)
        {
            _context = context;
            _tokenManager = tokenManager;
        }

        public List<Vehicle> GetAvailableVehicles(string token)
        {
            var claims = _tokenManager.ExtractClaims(token);
            var idClaim = claims.FirstOrDefault(x => x.Type == "ID");
            if (idClaim != null)
            {
                var userID = Convert.ToInt32(idClaim.Value);
                var personRepository = new GenericRepository<Person>(_context);
                var person = personRepository.GetAll(x => x.USER_ID == userID).FirstOrDefault();
                if (person == null || person.ROLE_ID != (int)RoleEnum.COURIER)
                {
                    throw new Exception("Nu aveti acces la aceasta informatie");
                }
            }
            else
            {
                throw new Exception("A aparut o eroare.");
            }
            var vehiclePersonRepository = new GenericRepository<PersonVehicle>(_context);
            var notAvailableVehiclesIDs = vehiclePersonRepository.GetAll(v => v.USE_DATE.Date == DateTime.Today).Select(v => v.VEHICLE_ID).ToList();
            var vehicleRepository = new GenericRepository<Vehicle>(_context);
            var availableVehicles = vehicleRepository.GetAll(v => !notAvailableVehiclesIDs.Contains(v.ID));
            return availableVehicles.ToList();
        }

        public List<City> GetCities()
        {
            var repository = new GenericRepository<City>(_context);
            return repository.GetAllIncluding(x => x.Courier).ToList();
        }
    }
}
