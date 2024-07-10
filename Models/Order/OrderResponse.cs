namespace  cyberforgepc.Models.Order
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
        public List<ProductResponse> Products { get; set; }
        public double? SubTotal { get; set; }
        public double? Total { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
