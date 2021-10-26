using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApiCiCd.Helpers
{
    public class JwtService
    {
        private const string SecureKey = "co#kP3M/uS>$7^8PK6M8uY,q=est~'G>oI\"kz92MvbsvoSG~yFWyz5l^YV48arS";

        public string Generate(int id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(SecureKey));
            var credentials = new SigningCredentials(
                symmetricSecurityKey, 
                SecurityAlgorithms.HmacSha256Signature);
            
            var header = new JwtHeader(credentials);
            // jwt token expires after 1 day
            var payload = new JwtPayload(
                id.ToString(), null, null, null, 
                DateTime.Today.AddDays(1));
            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecureKey);

            tokenHandler.ValidateToken(
                jwt,
                new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                },
                out var validatedToken);

            return validatedToken as JwtSecurityToken;
        }
    }
}