namespace  cyberforgepc.Models.Coupon
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CouponRequest
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public int Discount { get; set; }
        [Required]
        public DateOnly ExpirationDate { get; set; }
    }

    public class CouponUpdateRequest
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public int Discount { get; set; }
        [Required]
        public DateOnly ExpirationDate { get; set; }
    }
}
