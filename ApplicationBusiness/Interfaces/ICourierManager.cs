using Models.Vehicles;

namespace ApplicationBusiness.Interfaces
{
    public interface ICourierManager
    {
        List<Vehicles> GetAvailableVehicles(string token);
    }
}