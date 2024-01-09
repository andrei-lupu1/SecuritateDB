using Models.Common;
using Models.Users;
using Models.Vehicles;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models.Person
{
    [Table("PERSONS")]
    public class Person: DomainModelBase, IDomainModel
    {

        public string NUME { get; set; }

        public string TELEFON { get; set; }

        public string EMAIL { get; set; }

        public int ROLE_ID { get; set; }

        public Roles Role { get; set; }

        public int USER_ID { get; set; }

        public Users.Users User { get; set; }
    }
}
