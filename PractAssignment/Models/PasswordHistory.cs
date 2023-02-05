namespace PractAssignment.Models;

public class PasswordHistory
{
    public ApplicationUser? ApplicationUser { get; set; }

    public DateTime LastChanged { get; set; }
    
    public string? LastPassword1 { get; set; }
    
    public string? LastPassword2 { get; set; }
}