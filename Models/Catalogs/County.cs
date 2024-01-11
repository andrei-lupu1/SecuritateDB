using Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Catalogs
{
    [Table("COUNTIES")]
    public class County: DomainModelBase, IDomainModel
    {
        public string NAME { get; set; }

        public IEnumerable<City> Cities { get; set; } 
    }
}
