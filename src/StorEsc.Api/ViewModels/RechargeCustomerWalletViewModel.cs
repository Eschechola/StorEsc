using System.ComponentModel.DataAnnotations;
using StorEsc.API.Attributes.Validation;

namespace StorEsc.API.ViewModels;

public class RechargeCustomerWalletViewModel
{
    [Required(ErrorMessage = "Amount can not be empty.")]
    [Range(10, 10000, ErrorMessage = "Amount can be between 10 and 10.000.")]
    public decimal Amount { get; set; }
    
    [Required(ErrorMessage = "HoldName can not be empty.")]
    [MinLength(5, ErrorMessage = "HoldName must be at least 5 characters.")]
    [MaxLength(40, ErrorMessage = "HoldName must have a maximum of 40 characters.")]
    public string HoldName { get; set; }
    
    [Required(ErrorMessage = "Credit card number can not be empty.")]
    [CreditCard(ErrorMessage = "Credit card number is invalid.")]
    public string Number { get; set; }
    
    [Required(ErrorMessage = "ExpirationDate can not be empty.")]
    [ExpirationDate(ErrorMessage = "ExpirationDate is invalid.")]
    public string ExpirationDate { get; set; }
    
    [Required(ErrorMessage = "CVV can not be empty.")]
    [Range(100, 999, ErrorMessage = "CVV is invalid.")]
    public int CVV { get; set; }
    
    [Required(ErrorMessage = "Document can not be empty.")]
    [Document(ErrorMessage = "Document is invalid.")]
    public string Document { get; set; }
}