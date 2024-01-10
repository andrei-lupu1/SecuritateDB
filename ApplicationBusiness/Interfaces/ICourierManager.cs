using Models.Catalogs;
using Models.Vehicles;

namespace ApplicationBusiness.Interfaces
{
    public interface ICourierManager
    {
        List<Vehicle> GetAvailableVehicles(string token);

        List<City> GetCities();
    }
}