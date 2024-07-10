namespace  cyberforgepc.Models.WishList
{
    using System.ComponentModel.DataAnnotations;

    public class WishListRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ProductId { get; set; }
    }

    public class WishListDeleteRequest
    {
        [Required]
        public string Id { get; set; }        
    }
}
