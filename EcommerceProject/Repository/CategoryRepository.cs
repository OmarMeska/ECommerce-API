using EcommerceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceProject.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private  EcommerceDbContext Context { get; set; }
        public CategoryRepository(EcommerceDbContext _context)
        {
            Context = _context;
        }

        public bool IsExist(int id )
        {
            return Context.categories.Any( c => c.Id == id );
        }


        public void Add(Category category)
        {
            Context.categories.Add(category);

        }

        public void DeleteById(int id)
        {

           Category CategoryToRemove =  Context.categories.FirstOrDefault(i => i.Id == id);
           Context.categories.Remove(CategoryToRemove);
        }

        public List<Category> GetAll()
        {
            return Context.categories.Include(p=>p.Products).ToList();
        }

        public Category GetById(int id)
        {
                Category category = Context.categories.Include(p=>p.Products).FirstOrDefault(i => i.Id == id);
            return category;
        }

        public Category GetByName(string name)
        {
             Category category = Context.categories.FirstOrDefault(category => category.Name == name);
            return category;
        }

        public void Update(Category category)
        {
            Category categoryToUpdate = Context.categories.FirstOrDefault(i => i.Id ==category.Id);
            categoryToUpdate.Name = category.Name; 
            categoryToUpdate.Products = category.Products;
       
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
