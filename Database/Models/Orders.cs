using System;
using System.Collections.Generic;

namespace cyberforgepc.Database.Models
{
    public partial class Orders
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string CouponId { get; set; }
        public string UserId { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public string State { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Coupons Coupon { get; set; }
        public virtual Products Product { get; set; }
        public virtual Users User { get; set; }
    }
}
