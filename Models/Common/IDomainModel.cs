using System.ComponentModel.DataAnnotations;

namespace Models.Common
{
    public interface IDomainModel
    {
        [Key]
        public int ID { get; set; }
    }
}
