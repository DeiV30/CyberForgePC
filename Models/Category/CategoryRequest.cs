namespace  cyberforgepc.Models.Category
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }

    public class CategoryUpdateRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
