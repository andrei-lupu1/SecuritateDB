using ApplicationBusiness.Interfaces;
using Models.Catalogs;
using Repository;
using Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationBusiness.CatalogsManager
{
    public class CatalogsManager : ICatalogsManager
    {
        private readonly Context _context;
        public CatalogsManager(Context context)
        {
            _context = context;
        }

        public List<County> GetCounties()
        {
            var countyRepository = new GenericRepository<County>(_context);
            var counties = countyRepository.GetAllIncluding(x => x.Cities);
            return counties.ToList();
        }

        public List<City> GetCities(int countyID)
        {
            var cityRepository = new GenericRepository<City>(_context);
            var cities = cityRepository.GetAll(x => x.COUNTY_ID == countyID);
            return cities.ToList();
        }
    }
}
