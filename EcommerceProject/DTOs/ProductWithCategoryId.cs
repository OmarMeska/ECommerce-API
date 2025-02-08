using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.DTOs
{
    public class ProductWithCategoryId
    {
        [Required]
        public int Id { get; set; }
        public string ?UserName { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
    }
}
