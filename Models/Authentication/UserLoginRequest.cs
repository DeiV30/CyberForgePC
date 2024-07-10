namespace  cyberforgepc.Models.Authentication
{
    using System.ComponentModel.DataAnnotations;

    public class UserLoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
