namespace  cyberforgepc.Helpers.Authentication
{
    using cyberforgepc.Database.Models;
    using cyberforgepc.Helpers.Authentication.Model;
    using cyberforgepc.Models.Authentication;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IAuthenticationHelper
    {
        AuthenticationData Authenticate(User user);
        AuthenticationData GenerateToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();        
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
