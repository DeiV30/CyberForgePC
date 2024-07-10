namespace  cyberforgepc.Helpers.Authentication.Model
{
    using System;
    
    public class AuthenticationData
    {
        public string Token { get; set; }
        public DateTime Expired { get; set; }
    }
}