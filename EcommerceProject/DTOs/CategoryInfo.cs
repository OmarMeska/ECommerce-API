using EcommerceProject.Models;
using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.DTOs
{
    public class CategoryInfo
    {
        [Required]
       public int Id { get; set; }

        
        public string Name { get; set; }
    }
}
