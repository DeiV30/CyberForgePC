using System;
using System.Collections.Generic;

namespace cyberforgepc.Database.Models;

public partial class User
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Discriminator { get; set; }

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }

    public string RefreshToken { get; set; }

    public DateTime? RefreshTimeStamp { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<WishList> WishLists { get; set; } = new List<WishList>();
}
