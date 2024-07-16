using System;
using System.Collections.Generic;

namespace cyberforgepc.Database.Models;

public partial class Product
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public string CategoryId { get; set; }

    public string Image { get; set; }

    public long Stock { get; set; }

    public string DeleteKey { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<WishList> WishLists { get; set; } = new List<WishList>();
}
