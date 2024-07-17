using System;

namespace cyberforgepc.Models.DashBoard
{
    public class LayoutResponse
    {
        public string TotalUser { get; set; }
        public string TotalOrder { get; set; }
    }

    public class LayoutResponse2
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string TransactionType { get; set; }
        public int Quantity { get; set; }
        public DateTime Created { get; set; }
    }
}
