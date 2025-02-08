using EcommerceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceProject.Repository
{
    public class ProductRepository : IProductRepository
    {

        private EcommerceDbContext Context { get; }
        public ProductRepository(EcommerceDbContext _context)
        {
            Context = _context;
        }

        public bool IsExist(int id)
        {
            return Context.products.Any(p=> p.Id == id);
        }
        public void Add(Product product)
        {
            Context.products.Add(product); 
        }

        public void DeleteById(int id,string UserId)
        {
            Product product = Context.products.Where(p=>p.UserId == UserId).FirstOrDefault(i => i.Id == id);
            Context.products.Remove(product);

        }

        public List<Product> GetAll(string UserId)
        {
            return Context.products.Include(c=>c.Category).Where(u=>u.UserId == UserId).ToList();
        }

        public Product GetById(int id,string UserId)
        {
            return Context.products.Include(c => c.Category).Where(u=>u.UserId == UserId).FirstOrDefault(i => i.Id == id);
        }

        public Product GetByName(string name)
        {
            return Context.products.Include(c => c.Category).FirstOrDefault(i => i.Name == name);
        }

        public void Save()
        {
          Context.SaveChanges();
        }

        public void Update( Product productRequist)
        {
         //Product productToUpdate  = Context.products.FirstOrDefault(i => i.Id == Id);
    
           Context.products.Update(productRequist);
        }

    }
}
