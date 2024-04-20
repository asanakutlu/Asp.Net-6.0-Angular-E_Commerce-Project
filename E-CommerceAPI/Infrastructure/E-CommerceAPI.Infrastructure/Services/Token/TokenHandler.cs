using E_CommerceAPI.Application.Abstractions.Token;
using E_CommerceAPI.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Application.DTOs.Token CreateAccessToken(int second, AppUser appUser)
        {
            Application.DTOs.Token token = new ();
            //security keyin simetriğini alıyoruz
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            //şifrelenmiş kimliği oluşturuyoruz
            SigningCredentials signinCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
            //oluşturulacak  token ayarlarını veriyoruz
            token.Expiration=DateTime.UtcNow.AddSeconds(second);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials:signinCredentials,
                claims:new List<Claim> { new(ClaimTypes.Name, appUser.UserName)}
                );
            //token olşturucu sınıfından bir öenk alalım
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            //string refreshToken = CreateRefreshToken();
            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
           using RandomNumberGenerator random= RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
            
        }
    }
}
