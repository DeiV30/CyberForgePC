namespace  cyberforgepc.Models.Product
{
    using cyberforgepc.Models.Category;
    using System;

    public class ProductResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public CategoryResponse Category { get; set; }
        public string Image { get; set; }
        public long Stock { get; set; }
        public string DeleteKey { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
