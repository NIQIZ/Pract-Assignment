using System.ComponentModel.DataAnnotations;

namespace PractAssignment.ViewModels;

public class RegisterView
{
    [Required]
    [DataType(DataType.Text)]
    public string? FullName { get; set; }
        
    [Required]
    [DataType(DataType.CreditCard)]
    public string? CreditCardNumber { get; set; }
        
        
    [Required]
    [DataType(DataType.Text)]
    public string? Gender { get; set; }
        
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string? MobileNumber { get; set; }
        
        
    [Required]
    [DataType(DataType.Text)]
    public string? DeliveryAddress { get; set; }
        
    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    public string? Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; }
        
    /*[DataType(DataType.Upload)]
    public IFormFile Photo { get; set; }*/

    [DataType(DataType.MultilineText)]
    public string? AboutMe { get; set; }


}