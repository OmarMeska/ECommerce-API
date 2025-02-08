using EcommerceProject.Models;

namespace EcommerceProject.DTOs
{
    public class UserInfo
    {
        public string Name { get; set; }
        
      
        public List<ProductInfo>? products { get; set; }
 
    }
}
