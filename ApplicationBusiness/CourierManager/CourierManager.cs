using ApplicationBusiness.Interfaces;
using Models.Catalogs;
using Models.Enums;
using Models.Orders;
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
            CheckCourierRights(token);
            var vehiclePersonRepository = new GenericRepository<PersonVehicle>(_context);
            var notAvailableVehiclesIDs = vehiclePersonRepository.GetAll(v => v.USE_DATE.Date == DateTime.Today).Select(v => v.VEHICLE_ID).ToList();
            var vehicleRepository = new GenericRepository<Vehicle>(_context);
            var availableVehicles = vehicleRepository.GetAll(v => !notAvailableVehiclesIDs.Contains(v.ID));
            return availableVehicles.ToList();
        }
        
        public List<Order> GetOrdersForCourier(string token)
        {
            var courierID = CheckCourierRights(token);
            var orderRepository = new GenericRepository<Order>(_context);
            var courierOrders = orderRepository.GetAllIncluding(o => o.COURIER_ID == courierID ,o => o.HistoryOrders, o => o.Customer , o => o.Customer.Address);
            var pickOrders = courierOrders.Where(o => o.HistoryOrders.Any(h => h.STATUS_ID == (int)StatusesEnum.AWBINITIAT) && o.HistoryOrders.Count() == 1);
            var deliverOrders = courierOrders.Where(o => o.HistoryOrders.Any(h => h.STATUS_ID == (int)StatusesEnum.INDEPOZIT) && o.HistoryOrders.Count() == 3);
            List<Order> orders = [.. pickOrders, .. deliverOrders];
            return orders;
        }

        public void CourierStartWorking(string token, int vehicleID)
        {
            var courierID = CheckCourierRights(token);
            var personVehicle = new PersonVehicle()
            {
                COURIER_ID = courierID,
                VEHICLE_ID = vehicleID,
                USE_DATE = DateTime.Today
            };
            var vehiclePersonRepository = new GenericRepository<PersonVehicle>(_context);
            _context.BeginTransaction();
            vehiclePersonRepository.Add(personVehicle);
            var orderRepository = new GenericRepository<Order>(_context);
            var courierOrders = orderRepository.GetAllIncluding(o => o.COURIER_ID == courierID, o => o.HistoryOrders);
            var deliverOrders = courierOrders.Where(o => o.HistoryOrders.Any(h => h.STATUS_ID == (int)StatusesEnum.INDEPOZIT) && o.HistoryOrders.Count() == 3);
            var historyOrderRepository = new GenericRepository<HistoryOrder>(_context);
            foreach (var order in deliverOrders)
            {
                var historyOrder = new HistoryOrder()
                {
                    ORDER_ID = order.ID,
                    STATUS_ID = (int)StatusesEnum.INCURSDELIVRARE,
                    STATUS_DATE = DateTime.Now,
                    LOCATION = order.HistoryOrders.Last().LOCATION
                };
                historyOrderRepository.Add(historyOrder);
            }
            _context.Save();
            _context.CommitTransaction();
        }

        public void MarkOrderAsDone(string token, int orderID)
        {
            var courierID = CheckCourierRights(token);
            var orderRepository = new GenericRepository<Order>(_context);
            var order = orderRepository.GetByIdIncluding(orderID, o => o.HistoryOrders);
            var historyOrderRepository = new GenericRepository<HistoryOrder>(_context);
            _context.BeginTransaction();
            if(order.HistoryOrders.Count() == 1)
            {
                var historyOrder = new HistoryOrder()
                {
                    ORDER_ID = order.ID,
                    STATUS_ID = (int)StatusesEnum.PRELUAT,
                    STATUS_DATE = DateTime.Now,
                    LOCATION = order.HistoryOrders.Last().LOCATION
                };
                historyOrderRepository.Add(historyOrder);
            }
            else
            {
                var historyOrder = new HistoryOrder()
                {
                    ORDER_ID = order.ID,
                    STATUS_ID = (int)StatusesEnum.LIVRAT,
                    STATUS_DATE = DateTime.Now,
                    LOCATION = order.HistoryOrders.Last().LOCATION
                };
                historyOrderRepository.Add(historyOrder);
            }
            _context.Save();
            _context.CommitTransaction();
        }

        private int CheckCourierRights(string token)
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
                return userID;
            }
            else
            {
                throw new Exception("A aparut o eroare.");
            }
        }
    }
}
