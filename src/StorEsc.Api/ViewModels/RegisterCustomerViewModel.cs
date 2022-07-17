using System.ComponentModel.DataAnnotations;

namespace StorEsc.Api.ViewModels;

public class RegisterCustomerViewModel
{
    [Required(ErrorMessage = "FirstName can not be empty.")]
    [MinLength(3, ErrorMessage = "FirstName must be at least 3 characters.")]
    [MaxLength(100, ErrorMessage = "FirstName must have a maximum of 100 characters.")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "LastName can not be empty.")]
    [MinLength(3, ErrorMessage = "LastName must be at least 3 characters.")]
    [MaxLength(100, ErrorMessage = "LastName must have a maximum of 100 characters.")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Email can not be empty.")]
    [MinLength(8, ErrorMessage = "Email must be at least 8 characters.")]
    [MaxLength(180, ErrorMessage = "Email must have a maximum of 200 characters.")]
    [EmailAddress(ErrorMessage = "Email invalid.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password can not be empty.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    [MaxLength(180, ErrorMessage = "Email must have a maximum of 120 characters.")]
    public string Password { get; set; }
}