using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace _20263.Model
{
	public class ApplicationUser : IdentityUser
	{
		[StringLength(60)]
		public string? DisplayName { get; set; }

		public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
		// Add any additional properties you need for your application user
	}
}