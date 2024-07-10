using System;
using System.Collections.Generic;

namespace cyberforgepc.Database.Models
{
    public partial class Products
    {
        public Products()
        {
            Orders = new HashSet<Orders>();
            WishLists = new HashSet<WishLists>();
        }

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

        public virtual Categories Category { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<WishLists> WishLists { get; set; }
    }
}
