using Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Catalogs
{
    [Table("CITIES")]
    public class City : DomainModelBase, IDomainModel
    {
        public string NUME { get; set; }
        [ForeignKey("County")]
        public int COUNTY_ID { get; set; }
        [JsonIgnore]
        public County County { get; set; }
        [ForeignKey("Courier")]
        public int COURIER_ID { get; set; }

        public Person.Person Courier { get; set; }
    }
}
