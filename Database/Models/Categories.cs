using System;
using System.Collections.Generic;

namespace cyberforgepc.Database.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
