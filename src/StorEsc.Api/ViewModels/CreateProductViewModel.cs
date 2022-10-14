using System.ComponentModel.DataAnnotations;

namespace StorEsc.API.ViewModels;

public class CreateProductViewModel
{
    [Required(ErrorMessage = "Name can not be empty.")]
    [MinLength(6, ErrorMessage = "Name must be at least 6 characters.")]
    [MaxLength(200, ErrorMessage = "Name must have a maximum of 200 characters.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Description can not be empty.")]
    [MinLength(100, ErrorMessage = "Description must be at least 100 characters.")]
    [MaxLength(2000, ErrorMessage = "Description must have a maximum of 2000 characters.")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Price can not be empty.")]
    [Range(5, 1000000, ErrorMessage = "Price can be between 5 and 1.000.000")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "Stock can not be empty.")]
    [Range(0, 1000000, ErrorMessage = "Stock can be between 0 and 1.000.000")]
    public int Stock { get; set; }
}