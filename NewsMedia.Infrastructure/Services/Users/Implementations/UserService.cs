using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsMedia.Domain.Models.Users;
using NewsMedia.Infrastructure.DTOS.Users;
using NewsMedia.Infrastructure.Services.Users.Abstractions;

namespace NewsMedia.Infrastructure.Services.Users.Implementations
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly IMapper _mapper;
        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        private async Task AddClaim(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "Moderator");
            }
        }
        public async Task ToggleModerator(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var role = await _userManager.GetRolesAsync(user);
            if (role.Count == 0)
            {
                await AddClaim(userId);
            }
            else
            {
                await RemoveClaim(userId);
            }
        }
        private async Task RemoveClaim(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                _userManager.RemoveFromRoleAsync(user, "Moderator");
            }
        }
        public async Task<IEnumerable<GetUserBasicData>> Users()
        {
            var user = await _userManager.GetUsersInRoleAsync("Admin");
            var users = await _userManager.Users.ToListAsync();
            users.Remove(user.FirstOrDefault());
            var maps = new List<GetUserBasicData>();
            var userWithRole = new GetUserBasicData();
            foreach (var item in users)
            {
                var roleName = await _userManager.GetRolesAsync(item);
                userWithRole = _mapper.Map<GetUserBasicData>(item);
                if (roleName.FirstOrDefault() == "Moderator")
                {
                    userWithRole.Role = "Moderator";
                }
                maps.Add(userWithRole);
            }
            return maps;
        }
    }
}
