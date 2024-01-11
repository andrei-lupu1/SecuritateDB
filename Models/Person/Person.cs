using Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Person
{
    [Table("PERSONS")]
    public class Person: DomainModelBase, IDomainModel
    {

        public string NUME { get; set; }

        public string TELEFON { get; set; }

        public string EMAIL { get; set; }

        public int? USER_ID { get; set; }

        public int? ROLE_ID { get; set; }

        public Address Address { get; set; }
    }
}
