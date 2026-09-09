using System.ComponentModel.DataAnnotations;

namespace _20263.DTOs.Identity
{
    public class RegisterUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;

        [Required]
        [StringLength(60)]
        public string DisplayName { get; set; } = default!;
    }
}
