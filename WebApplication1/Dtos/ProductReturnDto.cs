namespace WebApplication1.Dtos
{
    public class ProductReturnDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

       public int CategoryId { get; set; }
       public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public List<ProductColorDto>? ProductColors { get; set; }

    }
    public class ProductColorDto
    {
        public string ColorName { get; set; } = null!;
    }
}
