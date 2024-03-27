using NewsMedia.Domain.Models.Users;

namespace NewsMedia.Persistance.JWT
{
    public interface ITokenHandler
    {
        Task<Token> CreateAccessToken(AppUser appUser);
    }
}
