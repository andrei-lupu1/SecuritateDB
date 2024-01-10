using Models.Catalogs;
using Models.Orders;
using Models.Vehicles;

namespace ApplicationBusiness.Interfaces
{
    public interface ICourierManager
    {
        List<Vehicle> GetAvailableVehicles(string token);

        public List<Order> GetOrdersForCourier(string token);

        public void CourierStartWorking(string token, int vehicleID);

        public void MarkOrderAsDone(string token, int orderID);

    }
}