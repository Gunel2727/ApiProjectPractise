using ApiProjectPractise.Data;
using ApiProjectPractise.Dtos.ProductDtos;
using ApiProjectPractise.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjectPractise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(AppDbContext appDbContext,IMapper mapper) : ControllerBase
    {
            [HttpGet]
            public IActionResult GetProducts()
            {
                var products = appDbContext.Products.ToList();
                return Ok(products);
            }
           
            [HttpPost]
            public IActionResult AddProduct(ProductCreateDto productCreateDto)
            {
                    var newProduct = mapper.Map<Product>(productCreateDto);
                    appDbContext.Products.Add(newProduct);
                    appDbContext.SaveChanges();
                    return Ok(newProduct);
        }
            [HttpPut("{id}")]
            public IActionResult UpdateProduct(int id, Product product)
            {
                    var existingProduct = appDbContext.Products.Find(id);
                    if (existingProduct == null)
                    {
                        return NotFound();
                    }
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.CategoryId = product.CategoryId;
                    existingProduct.UpdatedDate = DateTime.Now;
                    appDbContext.SaveChanges();
                    return Ok(existingProduct);
            }
            [HttpDelete("{id}")]
            public IActionResult DeleteProduct(int id)
            {
                    var product = appDbContext.Products.Find(id);
                    if (product == null)
                    {
                        return NotFound();
                    }
                    appDbContext.Products.Remove(product);
                    appDbContext.SaveChanges();
                    return Ok();
        }
    }
}
