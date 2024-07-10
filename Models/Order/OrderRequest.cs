namespace  cyberforgepc.Models.Order
{
    using System.ComponentModel.DataAnnotations;

    public class OrderRequest
    {
        [Required]
        public string ProductId { get; set; }
        public string CouponId { get; set; }
        public string UserId { get; set; }
        public double SubTotal{ get; set; }
        public double Total { get; set; }
    }
}
