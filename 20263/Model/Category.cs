using System.ComponentModel.DataAnnotations;

namespace _20263.Model
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
