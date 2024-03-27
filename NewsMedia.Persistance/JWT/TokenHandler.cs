using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NewsMedia.Domain.Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewsMedia.Persistance.JWT
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;
        readonly UserManager<AppUser> _userManager;
        public TokenHandler(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<Token> CreateAccessToken(AppUser user)
        {
            Token token = new Token();

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            DateTime expireDate = DateTime.UtcNow.AddHours(Double.Parse(_configuration["Token:ExpireDate"]));
            var claims = await SetClaims(user);
            JwtHeader jwtHeader = new JwtHeader(signingCredentials: signingCredentials);
            JwtPayload jwtPayload = new JwtPayload(audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issure"],
                expires: expireDate,
                notBefore: DateTime.UtcNow,
                claims: claims);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(payload: jwtPayload, header: jwtHeader);



            /*
               Payload = new JwtPayload(issuer, audience, claims, notBefore, expires);
            Header = new JwtHeader(signingCredentials);
            RawSignature = string.Empty;*/
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);
            return token;
        }

        private async Task<IEnumerable<Claim>> SetClaims(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Surname, user.FullName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));
            return claims;
        }
    }
}
