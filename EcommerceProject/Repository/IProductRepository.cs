using EcommerceProject.Models;

namespace EcommerceProject.Repository
{
    public interface IProductRepository
    {
        public Product GetById(int id,string UserId);
        public Product GetByName(string name);
        public List<Product> GetAll(string UserId);

       public  bool IsExist(int id);
        public void DeleteById(int id,string UserId);

        public void Add(Product product);
        public void Update( Product product );
        public void Save();
    }
}
