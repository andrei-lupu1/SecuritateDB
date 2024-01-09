using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Models.Common;

namespace Models.Users
{
    [Table("USERS")]
    public class Users: DomainModelBase, IDomainModel
    {

        public string USERNAME { get; set; } 

        public byte[] PASS { get; set; }
        [JsonIgnore]
        public Person.Person Person { get; set; }
    }
}
