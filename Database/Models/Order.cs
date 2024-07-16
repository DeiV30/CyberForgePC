using System;
using System.Collections.Generic;

namespace cyberforgepc.Database.Models;

public partial class Order
{
    public string Id { get; set; }

    public string CouponId { get; set; }

    public string UserId { get; set; }

    public double SubTotal { get; set; }

    public double Total { get; set; }

    public string State { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual Coupon Coupon { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; }
}
