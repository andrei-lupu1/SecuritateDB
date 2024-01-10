using ApplicationBusiness.Interfaces;
using DataTransformationObjects.Payloads;
using Models.Enums;
using Models.Orders;
using Models.Person;
using Repository;
using Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationBusiness.CustomerManager
{
    public class CustomerManager : ICustomerManager
    {
        private readonly Context _context;
        private readonly TokenManager.TokenManager _tokenManager;

        public CustomerManager(Context context, TokenManager.TokenManager tokenManager)
        {
            _context = context;
            _tokenManager = tokenManager;
        }

        public Order AddOrder(string token, OrderPayload orderPayload)
        {
            var customerID = CheckCustomerRights(token);

            #region Add new Address

            var address = new Address
            {
                ADDRESS = orderPayload.Address.ADDRESS,
                CITY_ID = orderPayload.Address.CITY_ID,
                ZIP_CODE = orderPayload.Address.ZIP_CODE,
            };
            var addressRepository = new GenericRepository<Address>(_context);
            _context.BeginTransaction();
            addressRepository.Add(address);
            _context.CommitTransaction();
            _context.SaveChanges();

            #endregion

            #region Add new Order

            var order = new Order
            {
                AMMOUNT = orderPayload.Ammount,
                DESCRIPTION = orderPayload.Description,
                PAYMENT_METHOD_ID = orderPayload.PaymentMethodId,
                CUSTOMER_ID = customerID,
                PIN_CODE = new Random().Next(1000, 9999),
                DELIVERY_ADDRESS_ID = address.ID,
            };
            var orderRepository = new GenericRepository<Order>(_context);
            _context.BeginTransaction();
            orderRepository.Add(order);

            #endregion

            #region Add new HistoryOrder

            var historyOrder = new HistoryOrder
            {
                ORDER_ID = order.ID,
                STATUS_ID = (int)StatusesEnum.AWBINITIAT,
                STATUS_DATE = DateTime.Now,
                LOCATION = address.City.NUME
            };
            var historyOrderRepository = new GenericRepository<HistoryOrder>(_context);
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
            var orders = orderRepository.GetAllIncluding(x => x.CUSTOMER_ID == customerID, x => x.HistoryOrders, x => x.Courier).ToList();
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
                return userID;
            }
            else
            {
                throw new Exception("A aparut o eroare.");
            }
        }
    }
}
