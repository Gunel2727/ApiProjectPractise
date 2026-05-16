using ApiProjectPractise.Attributes;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace ApiProjectPractise.Dtos.CategoryDtos
{
    public class CategoryCreateDto
    {
        //[MaxLength(100)]
        public string Name { get; set; }=null!;
        public string Description { get; set; }=null!;
        //[FileTypes ("image/jpeg", "image/png"]
        //[FileLength(2)]
        public IFormFile Photo { get; set; }=null!;
    }
    public class CategoryCreateDtoValidator:AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

            RuleFor(x => x.Description)
           .NotEmpty()
           .MinimumLength(5);

            RuleFor(x => x.Photo)
            .NotNull()
            .Must(x =>
                x.ContentType == "image/jpeg" ||
                x.ContentType == "image/png")
            .WithMessage("Yalnız jpeg və png faylları qəbul olunur.")
            .Must(x => x.Length <= 2 * 1024 * 1024)
            .WithMessage("Fayl maksimum 2MB ola bilər.");

        }
    }
}
