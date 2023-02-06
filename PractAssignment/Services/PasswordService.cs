using PractAssignment.Models;

namespace PractAssignment.Services;

using Microsoft.AspNetCore.Identity;

public class PasswordService
{
    public Task<bool> CheckPasswordHistory(string newPassword, string? currentPassword, string? lastPassword1, string? lastPassword2)
    {
        //True == Password does not exist
        //False == Password exists
        bool passwordExists;

        if (lastPassword1 == null)
        {
            lastPassword1 = String.Empty;
        }
        
        if (lastPassword2 == null)
        {
            lastPassword2 = String.Empty;
        }
        
        var passwordHasher = new PasswordHasher<ApplicationUser>();
        
        PasswordVerificationResult result1 =
            passwordHasher.VerifyHashedPassword(null, currentPassword, newPassword);
        
        PasswordVerificationResult result2 =
            passwordHasher.VerifyHashedPassword(null, lastPassword1, newPassword);
        
        PasswordVerificationResult result3 =
            passwordHasher.VerifyHashedPassword(null, lastPassword2, newPassword);

        if ((result1 == PasswordVerificationResult.Success) || (result2 == PasswordVerificationResult.Success) || (result3 == PasswordVerificationResult.Success))
        {
            passwordExists = false; // Password is in the history
        }
        else
        {
            passwordExists = true; // Password is not in the history
        }

        return Task.FromResult(passwordExists);
    }
}