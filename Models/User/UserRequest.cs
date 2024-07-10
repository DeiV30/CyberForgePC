namespace  cyberforgepc.Models.User
{
    using System.ComponentModel.DataAnnotations;

    public class UserInsertRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }        
        [Required]
        public string Password { get; set; }
    }

    public class UserUpdateRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }            
    }
}
