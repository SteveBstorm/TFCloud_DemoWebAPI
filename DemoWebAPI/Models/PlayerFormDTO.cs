using System.ComponentModel.DataAnnotations;

namespace DemoWebAPI.Models
{
    public class PlayerFormDTO
    {
        [Required]
        [MinLength(4, ErrorMessage = "Doit contenir 4 caractères minimum")]
        [MaxLength(50)]
        public string Nickname { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(50)]

        public string Email { get; set; }
    }
}
