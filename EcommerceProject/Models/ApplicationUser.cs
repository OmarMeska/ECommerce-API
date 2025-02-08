using Microsoft.AspNetCore.Identity;

namespace EcommerceProject.Models
{
    public class ApplicationUser:IdentityUser
    {
       public string?Address { get; set; }
        public List <Product>? Products { get; set; }
    }
}
