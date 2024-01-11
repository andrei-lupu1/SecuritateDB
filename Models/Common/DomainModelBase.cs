using System.ComponentModel.DataAnnotations;

namespace Models.Common
{
    public class DomainModelBase : IDomainModel
    {
        [Key]
        public int ID { get; set; }
    }
}
