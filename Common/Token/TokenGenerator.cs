using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common.Token
{
    public class TokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public TokenGenerator(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<Token> CreateToken(AppUser user)
        {
            Token token = new Token();
            DateTime expiration = DateTime.Now.AddDays(7);
            token.TokenExpiration = expiration;

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>
            {
                new Claim("UserId",user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);

            for (int i = 0; i < roles.Count; i++)
            {
                claims.Add(new Claim
                (
                   ClaimTypes.Role, roles[i]
                ));
            }

            JwtSecurityToken securityToken = new JwtSecurityToken
                (
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: expiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials,
                claims: claims
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = Guid.NewGuid().ToString();
            token.RefreshTokenExpireDate = DateTime.Now.AddDays(7).AddMinutes(30);

            return token;
        }
    }
}
