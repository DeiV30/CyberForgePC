using System;
using System.Collections.Generic;

namespace cyberforgepc.Database.Models;

public partial class Coupon
{
    public string Id { get; set; }

    public string Code { get; set; }

    public int Discount { get; set; }

    public DateOnly ExpirationDate { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
