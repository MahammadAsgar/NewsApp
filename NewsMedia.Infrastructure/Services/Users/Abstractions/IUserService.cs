using NewsMedia.Infrastructure.DTOS.Users;

namespace NewsMedia.Infrastructure.Services.Users.Abstractions
{
    public interface IUserService
    {
        Task<IEnumerable<GetUserBasicData>> Users();
        Task ToggleModerator(int userId);
    }
}
