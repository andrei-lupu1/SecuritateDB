using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Users
{
    [Table("USERS")]
    public class Users
    {
        [Key]
        public int ID { get; set; }

        public string USERNAME { get; set; } 

        public byte[] PASS { get; set; }
    }
}
