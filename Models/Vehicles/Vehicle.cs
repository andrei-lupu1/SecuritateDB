using Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Vehicles
{
    [Table("VEHICLES")]
    public class Vehicle: DomainModelBase, IDomainModel
    {

        public string NUMBER_PLATE { get; set; }
    }
}
