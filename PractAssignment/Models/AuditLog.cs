namespace PractAssignment.Models;

public class AuditLog
{
    public int Id { get; set; }
    
    public ApplicationUser? User { get; set; }
    
    public string? Action { get; set; }
    
    public DateTime ActionTime { get; set; }
}