using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models.Users
{
    [Table("ROLES")]
    public class Roles
    {
        [Key]
        public int ID { get; set; }

        public string ROL { get; set; }

        public string DESCRIERE { get; set; }
        [JsonIgnore]
        public ICollection<Person.Person> People { get; set; }
    }
}
