using System.ComponentModel.DataAnnotations;

namespace NewsMedia.Infrastructure.DTOS.Users
{
    public record SignUpDTO
    {
        public string FullName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        [Compare("Password", ErrorMessage = "password must be equal")]
        public string ConfirmPassword { get; init; }
    }
}
