using ApplicationBusiness.Interfaces;
using ApplicationBusiness.TokenManager;
using Models.Enums;
using Models.Person;
using Models.Users;
using Models.Vehicles;
using Repository;
using Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<Vehicles> GetAvailableVehicles(string token)
        {
            var claims = _tokenManager.ExtractClaims(token);
            var idClaim = claims.FirstOrDefault(x => x.Type == "ID");
            if (idClaim != null)
            {
                var userID = Convert.ToInt32(idClaim.Value);
                var personRepository = new GenericRepository<Person>(_context);
                var person = personRepository.GetIncluding(x => x.Role).FirstOrDefault(x => x.USER_ID == userID);
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
            var vehicleRepository = new GenericRepository<Vehicles>(_context);
            var availableVehicles = vehicleRepository.GetAll(v => !notAvailableVehiclesIDs.Contains(v.ID));
            return availableVehicles.ToList();
        }
    }
}
