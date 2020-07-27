using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace YOBA_Web.Models.JwtAuth
{
    /// <summary>
    /// Generator and validator JWT
    /// user access tokens
    /// </summary>
    public class JwtService
    {
        private readonly string _secret;
        private readonly string _expDate;

        public JwtService(IConfiguration config)
        {
            _secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
        }

        /// <summary>
        /// Token generator using HS256 algoritm
        /// including user email and id claims and
        /// secret identified on appsettings.json.
        /// Should be extended for domain roles of
        /// applcation model
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GenerateSecurityToken(string email, string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.NameIdentifier, id)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Token security validator.
        /// Based on appsettings.json params as
        /// token lifetime and claims from
        /// authenticationExtension
        /// </summary>
        /// <param name="jwtToken"></param>
        /// <returns></returns>
        public ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;
            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();
            validationParameters.ValidateLifetime = true;
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);
            return principal;
        }
    }
}
