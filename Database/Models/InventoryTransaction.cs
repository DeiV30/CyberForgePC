using System;
using System.Collections.Generic;

namespace cyberforgepc.Database.Models;

public partial class InventoryTransaction
{
    public string Id { get; set; }

    public string ProductId { get; set; }

    public string TransactionType { get; set; }

    public int Quantity { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual Product Product { get; set; }
}
