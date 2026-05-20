using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class CategoryCreateViewModel
    {
        [Required]
        [MaxLength(100)]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(500)]
        [Display(Name = "Category Description")]
        public string Description { get; set; } = null!;

        [Required]
        [Display(Name = "Category Image")]
        public IFormFile File { get; set; } = null!;
    }
}
