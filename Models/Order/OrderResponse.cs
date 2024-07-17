namespace cyberforgepc.Models.Order
{
    using cyberforgepc.Models.Coupon;
    using cyberforgepc.Models.Product;
    using cyberforgepc.Models.User;
    using System;
    using System.Collections.Generic;

    public class OrderResponse
    {
        public string Id { get; set; }
        public CouponResponse Coupon { get; set; }
        public UserResponse User { get; set; }
        public double? Total { get; set; }
        public double SubTotal { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }

    public class OrderItemResponse
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }

    }
}
