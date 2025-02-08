using EcommerceProject.DTOs;
using EcommerceProject.Models;
using EcommerceProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceProject.Controllers
{
    [Route("api/[controller]")]
        [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository ProductRepo { get; }
        public ProductController(IProductRepository products )
        {
            ProductRepo = products;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<Product> products = ProductRepo.GetAll(userId);
            if (!products.Any())
              return NoContent();

         List< ProductWithCategoryName> productInfo = new List<ProductWithCategoryName>();

            foreach (var item in products) { 

             ProductWithCategoryName ProductDto = new ProductWithCategoryName();
                ProductDto.Id = item.Id;
                ProductDto.UserName = User.FindFirst(ClaimTypes.Name).Value;
                
                ProductDto.Name = item.Name;
                ProductDto.Description = item.Description;
                ProductDto.Price = item.Price;
                ProductDto.CategoryName = item?.Category?.Name;

                productInfo.Add(ProductDto);
            }
            
            return Ok(productInfo);
        }


        [HttpGet]
        [Route("Id") ]
        public IActionResult GetById(int id)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!ProductRepo.IsExist(id)) return BadRequest("No Product Id Exist For You ");
            
            Product product = ProductRepo.GetById(id,UserId);
            
            ProductWithCategoryName productInfo = new ProductWithCategoryName();
            productInfo.UserName = User.FindFirst(ClaimTypes.Name).Value;
            productInfo.Id = id;
            productInfo.Name = product.Name;
            productInfo.Description = product.Description;
            productInfo.Price = product.Price;
            productInfo.CategoryName = product.Category?.Name;
            
            return Ok(productInfo);
        }

        [HttpDelete]
        public IActionResult DeleteById(int id) {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!ProductRepo.IsExist(id)) return BadRequest("Not Valid ID....");
            
            ProductRepo.DeleteById(id,userId);
            ProductRepo.Save();

            return Ok("Deleted");

        }


        [HttpPost] 
        public IActionResult AddProduct (ProductWithCategoryId productRequist)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Product product = new Product();
            product.UserId = userId;
            product.Id = productRequist.Id;
            product.Name = productRequist. Name;
            product.Description = productRequist. Description;
            product.Price = productRequist.Price;
            product.CategoryId = productRequist.CategoryId;

            ProductRepo.Add(product);
            ProductRepo.Save();
            return CreatedAtAction("GetById",product.Id, product);
        }

        [HttpPut]
        public IActionResult UpdateProduct(ProductInfo ProductRequist) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Product product = new Product();
            
            product = ProductRepo.GetById(ProductRequist.Id,UserId);
            product.Name = ProductRequist.Name;
            product.Description = ProductRequist.Description;
            product.Price = ProductRequist.Price;

            ProductRepo.Update( product);
            ProductRepo.Save();
            return Ok("Updated Succesfully ");
         }

    }
}
