using Microsoft.IdentityModel.Tokens;
using Northwind.Entity.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Bll
{
    public class TokenManager
    {
        public TokenManager()
        {

        }
        public string CreateAccessToken(DtoLoginUser user)
        {
            //claim
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserCode),
                new Claim(JwtRegisteredClaimNames.Jti,user.UserId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token");
            //claim Role
            var claimsRoleList = new List<Claim>
            {
            new Claim("role","Admin")
            //new Claim("role","Admin2"),
            };
            //security key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]));
            //Şifrelenmiş kimlik oluşturmak

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //token ayarları
            var token = new JwtSecurityToken
            (
                issuer:configuration["Tokens:Issuer"],
                audience:,
                expires:,
                notBefore:,
                signingCredentials:cred,
                claims:claimsIdentity.Claims
            );

            //token oluşturma sınıfı ile örnek alıp üretmek
            var tokenHandler = new { token = new JwtSecurityTokenHandler().WriteToken(token) };

            return tokenHandler.token;

        }
    }
}
