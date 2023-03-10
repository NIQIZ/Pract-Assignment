using Microsoft.AspNetCore.Identity;

namespace PractAssignment.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }

    public string? CreditCardNumber { get; set; }

    public string? Gender { get; set; }

    public string? MobileNumber { get; set; }

    public string? DeliveryAddress { get; set; }

    public byte[]? Photo { get; set; }

    public string? AboutMe { get; set; }

    public DateTime LastChanged { get; set; }
    
    public string? LastPassword1 { get; set; }
    
    public string? LastPassword2 { get; set; }
}
