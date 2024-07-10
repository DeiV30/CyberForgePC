namespace  cyberforgepc.Helpers.Middleware
{
    using Microsoft.AspNetCore.Authorization;

    public class AuthorizeMultipleAttribute : AuthorizeAttribute
    {
        public AuthorizeMultipleAttribute(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
