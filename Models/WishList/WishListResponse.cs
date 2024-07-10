namespace  cyberforgepc.Models.WishList
{
    using cyberforgepc.Models.Product;
    using cyberforgepc.Models.User;
    using System;
    using System.Collections.Generic;

    public class WishListResponse
    {        
        public string Id { get; set; }
        public ProductResponse Product{ get; set; }                
        public DateTime Created { get; set; }
    }

}
