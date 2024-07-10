using System;
using System.Collections.Generic;

namespace cyberforgepc.Database.Models
{
    public partial class WishLists
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Products Product { get; set; }
        public virtual Users User { get; set; }
    }
}
