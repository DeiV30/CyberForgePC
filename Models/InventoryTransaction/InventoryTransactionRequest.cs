namespace  cyberforgepc.Models.Category
{
    using System.ComponentModel.DataAnnotations;

    public class InventoryTransactionRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
