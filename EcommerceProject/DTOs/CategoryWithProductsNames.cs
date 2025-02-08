namespace EcommerceProject.DTOs
{
    public class CategoryWithProductsNames
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List <string>? ProductsNames { get; set; }
    }
}
