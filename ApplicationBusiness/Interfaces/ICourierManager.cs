using DataTransformationObjects.Payloads;
using Models.Orders;
using Models.Vehicles;

namespace ApplicationBusiness.Interfaces
{
    public interface ICourierManager
    {
        List<Vehicle> GetAvailableVehicles(string token);

        List<OrderOutput> GetOrdersForCourier(string token);

        void CourierStartWorking(string token, int vehicleID);

        void MarkOrderAsDone(string token, int orderID);

        void CourierFinishWorking(string token);

    }
}