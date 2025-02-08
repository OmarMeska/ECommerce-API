using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.DTOs
{
    public class ProductWithCategoryName
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? UserName { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
    }
}
