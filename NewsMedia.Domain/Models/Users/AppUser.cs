using Microsoft.AspNetCore.Identity;

namespace NewsMedia.Domain.Models.Users
{
    public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
