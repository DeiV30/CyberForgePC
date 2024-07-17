namespace  cyberforgepc.Models.Category
{
    using System;

    public class InventoryTransactionResponse
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string TransactionType { get; set; }
        public int Quantity { get; set; }
        public DateTime Created { get; set; }
    }

}
