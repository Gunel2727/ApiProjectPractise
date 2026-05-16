using ApiProjectPractise.Data;
using ApiProjectPractise.Dtos.CategoryDtos;
using ApiProjectPractise.Extensions;
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
    public class CategoryController(AppDbContext appDbContext,IMapper mapper,IValidator<CategoryCreateDto> validator) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var categories = appDbContext.Categories
                .Include(c => c.Products)
                .ToList();
            var categoryDtos = mapper.Map<List<CategoryReturnDto>>(categories);
            return Ok(categoryDtos);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = appDbContext.Categories
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryDto = mapper.Map<CategoryReturnDto>(category);
            return Ok(categoryDto);

        }
            [HttpPost]
            public IActionResult Post(CategoryCreateDto categoryCreateDto)
            {
               var validationResult=validator.Validate(categoryCreateDto);
                if (!validationResult.IsValid)
                {
                return BadRequest(validationResult.Errors);
                 }

            var newCategory = mapper.Map<Category>(categoryCreateDto);
                
                 appDbContext.Categories.Add(newCategory);
                appDbContext.SaveChanges();
                return Ok(newCategory);
             }
         [HttpPut("{id}")]
         public IActionResult Put(int id, CategoryUpdateDto categoryUpdateDto)
        {
                var existingCategory = appDbContext.Categories.Find(id);
                if (existingCategory == null)
                {
                    return NotFound();
                }
                mapper.Map(categoryUpdateDto, existingCategory);
                appDbContext.SaveChanges();
                return Ok();
        }
            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                    var category = appDbContext.Categories.Find(id);
                    if (category == null)
                    {
                        return NotFound();
                    }
                    appDbContext.Categories.Remove(category);
                    appDbContext.SaveChanges();
                    return Ok();
        }

    }
}
