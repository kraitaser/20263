using System.ComponentModel.DataAnnotations;

namespace _20263.DTOs.Identity
{
    public class UserCredentialsDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
    }
}
