using ApplicationBusiness.Interfaces;
using DataTransformationObjects.Payloads;
using Models.Catalogs;
using Models.Enums;
using Models.Orders;
using Models.Person;
using Repository;
using Repository.GenericRepository;

namespace ApplicationBusiness.CustomerManager
{
    public class CustomerManager : ICustomerManager
    {
        private readonly Context _context;
        private readonly ITokenManager _tokenManager;

        public CustomerManager(Context context, ITokenManager tokenManager)
        {
            _context = context;
            _tokenManager = tokenManager;
        }

        public Order AddOrder(string token, OrderPayload orderPayload)
        {
            var customerID = CheckCustomerRights(token);

            #region Add new Person

            var person = new Person
            {
                NUME = orderPayload.RecipientName,
                TELEFON = orderPayload.RecipientPhone,
                EMAIL = orderPayload.RecipientEmail,
                Address = new Address
                {
                    ADDRESS = orderPayload.Address,
                    ZIP_CODE = orderPayload.ZipCode,
                    CITY_ID = orderPayload.CityId
                },
                USER_ID = null,
                ROLE_ID = null
            };

            var personRepository = new GenericRepository<Person>(_context);
            _context.BeginTransaction();
            personRepository.Add(person);
            _context.CommitTransaction();
            _context.SaveChanges();

            #endregion

            #region Add new Order
            var cityRepository = new GenericRepository<City>(_context);
            var city = cityRepository.GetByIdIncluding(orderPayload.CityId, x => x.County);


            var order = new Order
            {
                AMMOUNT = orderPayload.Ammount,
                DESCRIPTION = orderPayload.Description,
                PAYMENT_METHOD_ID = orderPayload.PaymentMethodId,
                PIN_CODE = new Random().Next(1000, 9999),
                CUSTOMER_ID = person.ID,
                COURIER_ID = city.COURIER_ID,
                SENDER_ID = customerID
            };
            var orderRepository = new GenericRepository<Order>(_context);
            _context.BeginTransaction();
            orderRepository.Add(order);
            _context.CommitTransaction();
            _context.SaveChanges();
            #endregion

            #region Add new HistoryOrder

            var historyOrder = new HistoryOrder
            {
                ORDER_ID = order.ID,
                STATUS_ID = (int)StatusesEnum.AWBINITIAT,
                STATUS_DATE = DateTime.Now,
                LOCATION = city.County.NAME
            };
            var historyOrderRepository = new GenericRepository<HistoryOrder>(_context);
            _context.BeginTransaction();
            historyOrderRepository.Add(historyOrder);
            _context.CommitTransaction();
            _context.SaveChanges();

            #endregion

            return order;
        }

        public List<Order> GetOrdersForCustomer(string token)
        {
            var customerID = CheckCustomerRights(token);
            var orderRepository = new GenericRepository<Order>(_context);
            var orders = orderRepository.GetAllIncluding(x => x.SENDER_ID == customerID, x => x.HistoryOrders, x => x.Courier, x => x.Customer).ToList();
            return orders;
        }

        private int CheckCustomerRights(string token)
        {
            var claims = _tokenManager.ExtractClaims(token);
            var idClaim = claims.FirstOrDefault(x => x.Type == "ID");
            if (idClaim != null)
            {
                var userID = Convert.ToInt32(idClaim.Value);
                var personRepository = new GenericRepository<Person>(_context);
                var person = personRepository.GetAll(x => x.USER_ID == userID).FirstOrDefault();
                if (person == null || person.ROLE_ID != (int)RoleEnum.CLIENT)
                {
                    throw new Exception("Nu aveti acces la aceasta informatie");
                }
                return person.ID;
            }
            else
            {
                throw new Exception("A aparut o eroare.");
            }
        }
    }
}
