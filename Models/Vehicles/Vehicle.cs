using Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Vehicles
{
    [Table("VEHICLES")]
    public class Vehicle: DomainModelBase, IDomainModel
    {

        public string NUMBER_PLATE { get; set; }
    }
}
