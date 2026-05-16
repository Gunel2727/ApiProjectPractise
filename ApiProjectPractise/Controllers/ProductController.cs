using ApiProjectPractise.Data;
using ApiProjectPractise.Dtos.CategoryDtos;
using ApiProjectPractise.Dtos.ProductDtos;
using ApiProjectPractise.Models;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProjectPractise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(AppDbContext appDbContext,IMapper mapper,IValidator<ProductCreateDto> validator) : ControllerBase
    {
            [HttpGet]
            public IActionResult GetProducts()
            {
                var products = appDbContext.Products
                .Include(p => p.Category)
                .ToList();
                    var productDtos = mapper.Map<List<ProductReturnDto>>(products);
                    return Ok(productDtos);
            
            }
            [HttpGet("{id}")]
            public IActionResult GetProduct(int id) {
                var product = appDbContext.Products
                    .Include(p => p.Category)
                    .FirstOrDefault(p => p.Id == id);
                        if (product == null)
                        {
                            return NotFound();
                        }
                        var productDto = mapper.Map<ProductReturnDto>(product);
                        return Ok(productDto);
            }

        [HttpPost]
            public IActionResult AddProduct(ProductCreateDto productCreateDto)
            {
                var validationResult = validator.Validate(productCreateDto);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
            var category= appDbContext.Categories.Find(productCreateDto.CategoryId);
                    if (category == null)
                    {
                        return BadRequest("Invalid CategoryId");
                    }
                 var newProduct = mapper.Map<Product>(productCreateDto);
                    appDbContext.Products.Add(newProduct);
                    appDbContext.SaveChanges();
                    return Ok(newProduct);
             }
            [HttpPut("{id}")]
            public IActionResult UpdateProduct(int id, ProductUpdateDto productUpdateDto)
            {
                    var existingProduct = appDbContext.Products.Find(id);
                    if (existingProduct == null)
                    {
                        return NotFound();
                    }
                   mapper.Map(productUpdateDto, existingProduct);
                    appDbContext.SaveChanges();
                    return Ok();
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
