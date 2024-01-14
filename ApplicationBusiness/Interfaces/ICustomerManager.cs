using DataTransformationObjects.Orders;
using DataTransformationObjects.Payloads;
using Models.Orders;

namespace ApplicationBusiness.Interfaces
{
    public interface ICustomerManager
    {
        OrderOutput AddOrder(string token, OrderPayload orderPayload);

        List<OrderOutput> GetOrdersForCustomer(string token);
    }
}