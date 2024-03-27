using Microsoft.AspNetCore.Identity;
using NewsMedia.Domain.Models.Users;
using NewsMedia.Infrastructure.DTOS.Users;
using NewsMedia.Persistance.JWT;

namespace NewsMedia.Infrastructure.Services.Users.Abstractions
{
    public interface IAuthService
    {
        Task<Tuple<AppUser, string>> SignUp(SignUpDTO signUpDTO);
        Task<IdentityResult> ConfirmEmail(int userId, string token);
        Task<Tuple<Token, string>> SignIn(SignInDto signInDto);
        Task SignOut();
    }
}
