using System;
using System.Collections.Generic;

namespace cyberforgepc.Database.Models;

public partial class OrderItem
{
    public string Id { get; set; }

    public string OrderId { get; set; }

    public string ProductId { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Udpdate { get; set; }

    public virtual Order Order { get; set; }

    public virtual Product Product { get; set; }
}
