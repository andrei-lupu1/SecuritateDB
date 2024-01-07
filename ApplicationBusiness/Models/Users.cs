using System.ComponentModel.DataAnnotations;

namespace ApplicationBusiness.Models
{
    public class Users
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Pass { get; set; }
    }
}
