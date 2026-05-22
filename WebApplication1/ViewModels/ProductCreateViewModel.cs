using System.ComponentModel.DataAnnotations;
using WebApplication1.Dtos;

namespace WebApplication1.ViewModels
{
    public class ProductCreateViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(500)]
        [Display(Name = "Product Description")]
        public string Description { get; set; } = null!;

        [Range(0, double.MaxValue)]
        [Display(Name = "Product Price")]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public List<int>? ColorIds { get; set; }=new List<int>();

        public List<CategoryReturnDto>? Categories { get; set; }
         public List<ColorReturnDto>? Colors { get; set; }
    }
}
