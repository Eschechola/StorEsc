using System.ComponentModel.DataAnnotations;

namespace StorEsc.API.ViewModels;

public class UpdateVoucherViewModel
{
    [Required(ErrorMessage = "Code can not be empty.")]
    [MinLength(3, ErrorMessage = "Code must be at least 3 characters.")]
    [MaxLength(80, ErrorMessage = "Code must have a maximum of 80 characters.")]
    public string Code { get; set; }
    
    [Required(ErrorMessage = "IsPercentageDiscount can not be empty.")]
    public bool IsPercentageDiscount { get; set; }
    
    public decimal? ValueDiscount { get; set; }
    
    public decimal? PercentageDiscount { get; set; }
}