using System.Collections.Generic;

namespace cyberforgepc.Models.Order
{
    using System.ComponentModel.DataAnnotations;

    public class OrderRequest
    {
        public string CouponId { get; set; }
        [Required]
        public string UserId { get; set; }
        public double Total { get; set; }
        public double SubTotal { get; set; }
        public List<OrderItemRequest> OrderItems { get; set; }
    }

    public class OrderItemRequest
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
