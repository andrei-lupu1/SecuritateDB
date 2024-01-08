using Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Person
{
    [Table("PERSONS")]
    public class Person
    {
        [Key]
        public int ID { get; set; }

        public string NUME { get; set; }

        public string TELEFON { get; set; }

        public string EMAIL { get; set; }

        public int ROLE_ID { get; set; }

        public Roles Role { get; set; }
    }
}
