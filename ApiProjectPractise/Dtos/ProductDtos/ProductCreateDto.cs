using FluentValidation;

namespace ApiProjectPractise.Dtos.ProductDtos
{
    public class ProductCreateDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public  List<int> ColorIds { get; set; }
    }
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .LessThanOrEqualTo(100000);

            RuleFor(x=>x.ColorIds)
                .NotEmpty()
                .Must(colorIds => colorIds.All(id => id > 0))
                .WithMessage("All color IDs must be greater than 0.");
        }

    }


    
}
