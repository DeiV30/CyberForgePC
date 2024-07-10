namespace  cyberforgepc.Models.User
{
    using System;

    public class UserResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }                
        public DateTime? RefreshTimeStamp { get; set; }         
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }

    public class UserResponseAuth
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
