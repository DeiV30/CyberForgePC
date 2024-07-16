namespace  cyberforgepc.Helpers.Authentication
{
    using cyberforgepc.Database.Models;
    using cyberforgepc.Helpers.Authentication.Model;
    using cyberforgepc.Helpers.Settings;
    using cyberforgepc.Models.Authentication;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    public class AuthenticationHelper : IAuthenticationHelper
    {
        private static readonly HttpClient client = new HttpClient();

        private readonly AppSettings appSettings;

        public AuthenticationHelper(IOptions<AppSettings> appSettings) => this.appSettings = appSettings.Value;

        public AuthenticationData Authenticate(User user)
        {
            var RemoveAcentuatioName = System.Web.HttpUtility.UrlDecode(System.Web.HttpUtility.UrlEncode(
                user.Name, Encoding.GetEncoding("iso-8859-7")));

            var claim = new Claim[] {
                new Claim(ClaimTypes.NameIdentifier , user.Id),
                new Claim(ClaimTypes.Role, user.Discriminator),
                new Claim(ClaimTypes.Name, RemoveAcentuatioName),
                new Claim(ClaimTypes.Email, user.Email)       
            };

            return GenerateToken(claim);
        }

        public AuthenticationData GenerateToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var expired = DateTime.UtcNow.AddHours(int.Parse(appSettings.TimeExpiredToken));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expired,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationData
            {
                Token = tokenHandler.WriteToken(token),
                Expired = expired,
            };
        }

        public string GenerateRefreshToken()
        {
            var random = new byte[32];

            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(random);
            return Convert.ToBase64String(random);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return null;

            return principal;
        }
    }
}
