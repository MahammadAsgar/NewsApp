using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NewsMedia.Domain.Models.Users;
using NewsMedia.Infrastructure.DTOS.Users;
using NewsMedia.Infrastructure.Services.Mail;
using NewsMedia.Infrastructure.Services.Users.Abstractions;
using NewsMedia.Persistance.JWT;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewsMedia.Infrastructure.Services.Users.Implementations
{
    public class AuthService : IAuthService
    {
        readonly UserManager<AppUser> _userManager;
        //readonly IMailService mail;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;
        readonly IMapper _mapper;
        readonly IConfiguration _configuration;
        readonly RoleManager<AppRole> _roleManager;
        readonly IMailService _mailService;

        public AuthService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, SignInManager<AppUser> signInManager,
            ITokenHandler tokenHandler, IConfiguration configuration, RoleManager<AppRole> roleManager, IMailService mailService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _configuration = configuration;
            _roleManager = roleManager;
            _mailService = mailService;
        }

        public async Task<Tuple<Token, string>> SignIn(SignInDto signInDto)
        {
            string roleName = string.Empty;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == signInDto.Email);
            if (!user.EmailConfirmed)
            {
                user.EmailConfirmed = true;
                _userManager.UpdateAsync(user);
            }
            Token token = new Token();
            if (user != null)
            {
                var response = await _signInManager.CheckPasswordSignInAsync(user, signInDto.Password, true);

                if (response.Succeeded)
                {
                    token = await _tokenHandler.CreateAccessToken(user);
                    await _signInManager.SignInWithClaimsAsync(user, true, GetClaimsPrincipal(token.AccessToken).Claims);
                }

                var role = await _userManager.GetRolesAsync(user);
                if (role.FirstOrDefault() == "Admin")
                {
                    roleName = "Admin";
                }

                else if (role.FirstOrDefault() == "Moderator")
                {
                    roleName = "Moderator";
                }
                else
                {
                    roleName = "";
                }
            }
            return new Tuple<Token, string>(token, roleName);
        }
        private ClaimsPrincipal GetClaimsPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"])),
                ValidIssuer = _configuration["Token:Issure"],
                ValidAudience = _configuration["Token:Audience"],
            };

            SecurityToken validatedToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            return principal;
        }
        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Tuple<AppUser, string>> SignUp(SignUpDTO signUpDTO)
        {
            var user = new AppUser() { Email = signUpDTO.Email, FullName = signUpDTO.FullName, UserName = Guid.NewGuid().ToString() };// _mapper.Map<AppUser>(signUpDTO);
            var response = await _userManager.CreateAsync(user, signUpDTO.Password);

            //if (response.Succeeded)
            //{
            //    await _userManager.AddToRoleAsync(user, "Admin");
            //}
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return new Tuple<AppUser, string>(user, token);
        }
        public async Task<IdentityResult> ConfirmEmail(int userId, string token)
        {

            var user = await _userManager.FindByIdAsync(userId.ToString());
            var response = await _userManager.ConfirmEmailAsync(user, token);
            return response;
        }
    }
}
