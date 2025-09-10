using System.ComponentModel.DataAnnotations;

namespace A2Template.Models
{
    public class Staff
    {
        [Key]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}