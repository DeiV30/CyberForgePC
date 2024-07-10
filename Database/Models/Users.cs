using System;
using System.Collections.Generic;

namespace cyberforgepc.Database.Models
{
    public partial class Users
    {
        public Users()
        {
            Orders = new HashSet<Orders>();
            WishLists = new HashSet<WishLists>();
        }

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

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<WishLists> WishLists { get; set; }
    }
}
