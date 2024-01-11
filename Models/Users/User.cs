using System.ComponentModel.DataAnnotations.Schema;
using Models.Common;

namespace Models.Users
{
    [Table("USERS")]
    public class User: DomainModelBase, IDomainModel
    {

        public string USERNAME { get; set; } 

        public byte[] PASS { get; set; }

    }
}
