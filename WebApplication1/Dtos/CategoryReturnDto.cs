namespace WebApplication1.Dtos
{
    public class CategoryReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<ProductInCategoryReturnDto>? Products { get; set; }

    }
    
    public class ProductInCategoryReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CategoryId { get; set; }

    }
}
