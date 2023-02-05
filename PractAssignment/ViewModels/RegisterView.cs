using System.ComponentModel.DataAnnotations;

namespace PractAssignment.ViewModels;

public class RegisterView
{
    [Required]
    [DataType(DataType.Text)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Invalid Full Name")]
    public string? FullName { get; set; }
        
    [Required]
    [DataType(DataType.CreditCard)]
    [RegularExpression(@"^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$", ErrorMessage = "Invalid Credit Card")]
    public string? CreditCardNumber { get; set; }
        
        
    [Required]
    [DataType(DataType.Text)]
    [RegularExpression(@"^(Male|Female)$", ErrorMessage = "Invalid Gender")]
    public string? Gender { get; set; }
        
    [Required]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^[689][0-9]{7}$", ErrorMessage = "Invalid Number")]
    public string? MobileNumber { get; set; }
        
        
    [Required]
    [DataType(DataType.Text)]
    [RegularExpression(@"^[a-zA-Z0-9-\s-#]+$", ErrorMessage = "Invalid Delivery Address")]
    public string? DeliveryAddress { get; set; }
        
    [Required]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^(([^<>()\[\]\\.,;:\s@']+(\.[^<>()\[\]\\.,;:\s@']+)*)|('.+'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Please Enter a valid email address")]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#\$%\^&\*_])[a-zA-Z\d!@#\$%\^&\*_]{12,}$", ErrorMessage = "Invalid Password")]
    public string? Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; }
        
    /*[DataType(DataType.Upload)]
    public IFormFile Photo { get; set; }*/

    [DataType(DataType.MultilineText)]
    [RegularExpression(@"^[a-zA-Z0-9-\s-#]+$", ErrorMessage = "Invalid About Me format")]
    public string? AboutMe { get; set; }


}