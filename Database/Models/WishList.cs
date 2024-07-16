using System;
using System.Collections.Generic;

namespace cyberforgepc.Database.Models;

public partial class WishList
{
    public string Id { get; set; }

    public string UserId { get; set; }

    public string ProductId { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual Product Product { get; set; }

    public virtual User User { get; set; }
}
