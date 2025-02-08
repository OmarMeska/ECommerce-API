using EcommerceProject.DTOs;
using EcommerceProject.Models;
using EcommerceProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository CategoryRepository { get; }
        public CategoryController(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        [HttpGet]
      
        public IActionResult GetAll()
        {
            List <Category> categories =  CategoryRepository.GetAll();
           List<CategoryWithProductsNames> categoryInfos = new List<CategoryWithProductsNames>();

            foreach (var category in categories)
            {
                CategoryWithProductsNames categoryInfo = new CategoryWithProductsNames();
                categoryInfo.Name = category.Name;
                categoryInfo.Id = category.Id; 
                
                List<string > productNames = new List<string>();
                foreach (var item in category.Products)
                      productNames .Add(item.Name);

                    categoryInfo.ProductsNames = productNames;
               
                categoryInfos.Add(categoryInfo);
            }
            return Ok(categoryInfos);
        }

        [HttpGet("Id")]
       
        public IActionResult GetById( int id )
        {
            Category category = CategoryRepository.GetById(id);
            if (category == null) return BadRequest("NO CATEGORY ID EXIST...");

            List<string> ProductNames = new List<string>();
            foreach (var item in category.Products)           
                ProductNames.Add(item.Name);

            
            CategoryWithProductsNames categoryinfo = new CategoryWithProductsNames(); 
            categoryinfo.Name = category.Name;
            categoryinfo.Id = category.Id;
            categoryinfo.ProductsNames = ProductNames;

            
            return Ok(categoryinfo);
        }

        [HttpDelete]
        public IActionResult DeleteById(int id) {
            if (!CategoryRepository.IsExist(id)) return BadRequest("NO Valid ID Exist...");
           CategoryRepository.DeleteById(id);
            CategoryRepository.Save();
            return Ok("Deleted");

        }

        [HttpPost]
        public IActionResult Add(CategoryInfo category) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Category category1 = new Category();
            category1.Name = category.Name;
            category1.Id = category.Id; 

            CategoryRepository.Add(category1);
            CategoryRepository.Save();
            return CreatedAtAction("GetById",category.Id,category);

        }

        [HttpPut]
        public IActionResult Update(CategoryInfo categoryRequist ) {
           if (!ModelState.IsValid ) return BadRequest(ModelState);
            Category category =  new Category();
            category.Name = categoryRequist.Name;
            category.Id = categoryRequist.Id;

            //Category categoryDB= CategoryRepository.GetById(category.Id);
           
                   

            CategoryRepository.Update(category);
            CategoryRepository.Save();
            return Ok("updated")
 ; 
        }
    }
}
