using System.ComponentModel.DataAnnotations;

namespace TestIgnatov.Models.ViewModels
{
    public class EditProduct
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}