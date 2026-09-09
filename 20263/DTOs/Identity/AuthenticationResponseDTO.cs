namespace _20263.DTOs.Identity
{
    public class AuthenticationResponseDTO
    {
        public string Token { get; set; } = default!;
        public DateTime expiration {  get; set; }

        public string UserId { get; set; } = default!;
    }
}
