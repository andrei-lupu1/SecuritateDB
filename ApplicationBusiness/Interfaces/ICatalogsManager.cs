using Models.Catalogs;

namespace ApplicationBusiness.Interfaces
{
    public interface ICatalogsManager
    {
        List<City> GetCities(int countyID);
        List<County> GetCounties();
    }
}