namespace ApiProjectPractise.Dtos.ProductDtos
{
    public class ProductReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }=null!;
        public string Description { get; set; }=null!;
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public CategoryInProductReturnDto Category { get; set; }=null!;
        public List<ColorsInProductReturnDto> ProductColors { get; set; }

    }
    public class CategoryInProductReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }=null!;
        public string Description { get; set; }=null!;
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class ColorsInProductReturnDto
    {
        public string ColorName { get; set; }=null!;
    }
}
