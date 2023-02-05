using System.ComponentModel.DataAnnotations;

namespace PractAssignment.ViewModels;

public class LoginView
{
    [Required]
    public string? Token { get; set; }
    
    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    
    public bool RememberMe { get; set; }
}