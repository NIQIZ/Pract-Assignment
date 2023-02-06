using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using PractAssignment.Models;
using PractAssignment.Services;

namespace PractAssignment.Areas.Identity.Pages.Account;

public class SetPassword : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly PasswordService _passwordService;

    public SetPassword(UserManager<ApplicationUser> userManager, PasswordService passwordService)
    {
        _userManager = userManager;
        _passwordService = passwordService;
    }

    [BindProperty] public InputModel Input { get; set; }
    
    [TempData]
    public string StatusMessage { get; set; }
    public class InputModel
    {
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^(([^<>()\[\]\\.,;:\s@']+(\.[^<>()\[\]\\.,;:\s@']+)*)|('.+'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Please Enter a valid email address")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#\$%\^&\*_])[a-zA-Z\d!@#\$%\^&\*_]{12,}$", ErrorMessage = "Invalid Password")]
        public string? OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#\$%\^&\*_])[a-zA-Z\d!@#\$%\^&\*_]{12,}$", ErrorMessage = "Invalid Password")]
        public string? NewPassword { get; set; }

        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }

    public IActionResult OnGet()
    {
        var cookie = Request.Cookies["ToResetExpired"];
        if (string.IsNullOrEmpty(cookie))
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userManager.FindByEmailAsync(Input.Email);
        if (user == null)
        {
            // Don't reveal that the user does not exist
            return RedirectToPage("./ResetPasswordConfirmation");
        }
        string? currentPassword = user.PasswordHash;

        if (!await _passwordService.CheckPasswordHistory(Input.NewPassword, user.LastPassword1, user.LastPassword2))
        {
            ModelState.AddModelError(string.Empty, "Please use another password that is not your last 2 password");
            return Page();
        }
        
        var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            foreach (var error in changePasswordResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
        user.LastPassword2 = user.LastPassword1;
        user.LastPassword1 = currentPassword;
        user.LastChanged = DateTime.Now;
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            StatusMessage = "Your password has been changed.";
            Response.Cookies.Delete("ToResetExpired");
            return RedirectToPage("Login");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return Page();
    }
}