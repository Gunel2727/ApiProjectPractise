using ApiProjectPractise.Data;
using ApiProjectPractise.Dtos.ColorDtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjectPractise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class ColorController(AppDbContext _context,IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var colors = _context.Colors
                .ToList();
            var colorDtos = mapper.Map<List<ColorReturnDto>>(colors);
            return Ok(colorDtos);
        }
    }
}
