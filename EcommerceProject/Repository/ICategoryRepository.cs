using EcommerceProject.Models;

namespace EcommerceProject.Repository
{
    public interface ICategoryRepository
    {
        public Category  GetById(int id);
        public Category GetByName(string name);
        public List<Category> GetAll();

        public bool IsExist(int id);

        public void DeleteById(int id);

        public void Add(Category category);
        public void Update(Category category);

        public void Save();
    }
}
