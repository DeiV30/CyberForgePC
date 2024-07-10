namespace  cyberforgepc.Models.Product
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ProductRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string CategoryId { get; set; }        
        public string Image { get; set; }
        [Required]
        public long Stock { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
