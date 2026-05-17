using ApiProjectPractise.Dtos.UserDtos;
using ApiProjectPractise.Models;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjectPractise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(
        IValidator<RegisterDto> validator,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IMapper mapper
        ) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var validationResult=validator.Validate(registerDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var user=await userManager.FindByNameAsync(registerDto.UserName);
            if (user is not null)
            {
                return BadRequest("UserName already exists");
            }
          user=mapper.Map<AppUser>(registerDto);
            var result=await userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            await userManager.AddToRoleAsync(user, "Member");
            return Ok("user registered successfully");         
        }

        //[HttpGet]
        //public async Task<IActionResult> CreateRole()
        //{
        //    await roleManager.CreateAsync(new IdentityRole("Member"));
        //    await roleManager.CreateAsync(new IdentityRole("Admin"));

        //    return Ok();
        //}
    }
}
