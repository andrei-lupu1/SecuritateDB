using Models.Common;
using System.ComponentModel.DataAnnotations.Schema;


namespace Models.Vehicles
{
    [Table("PERSONS_VEHICLES")]
    public class PersonVehicle : DomainModelBase, IDomainModel
    {
        public int COURIER_ID { get; set; }

        public int VEHICLE_ID { get; set; }

        public DateTime USE_DATE { get; set; }

    }
}
