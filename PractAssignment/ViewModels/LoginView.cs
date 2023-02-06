using System.ComponentModel.DataAnnotations;

namespace PractAssignment.ViewModels;

public class LoginView
{
    [Required]
    public string? Token { get; set; }
    
    [Required]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^(([^<>()\[\]\\.,;:\s@']+(\.[^<>()\[\]\\.,;:\s@']+)*)|('.+'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Please Enter a valid email address")]
    public string? Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#\$%\^&\*_])[a-zA-Z\d!@#\$%\^&\*_]{12,}$", ErrorMessage = "Invalid Password")]

    public string? Password { get; set; }
    
    public bool RememberMe { get; set; }
}