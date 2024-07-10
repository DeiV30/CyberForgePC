using System;
using System.Collections.Generic;

namespace cyberforgepc.Database.Models
{
    public partial class Coupons
    {
        public Coupons()
        {
            Orders = new HashSet<Orders>();
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public int Discount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
