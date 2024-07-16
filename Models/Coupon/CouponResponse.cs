namespace  cyberforgepc.Models.Coupon
{
    using System;

    public class CouponResponse
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public int Discount { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public DateTime Created { get; set; }
    }

}
