// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using PractAssignment.Models;
using PractAssignment.Services;

namespace PractAssignment.Areas.Identity.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PasswordService _passwordService;
        private readonly AuditLogService _auditLogService;

        public ResetPasswordModel(
            UserManager<ApplicationUser> userManager,
            PasswordService passwordService,
            AuditLogService auditLogService)
        {
            _userManager = userManager;
            _passwordService = passwordService;
            _auditLogService = auditLogService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DataType(DataType.EmailAddress)]
            [RegularExpression(@"^(([^<>()\[\]\\.,;:\s@']+(\.[^<>()\[\]\\.,;:\s@']+)*)|('.+'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Please Enter a valid email address")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#\$%\^&\*_])[a-zA-Z\d!@#\$%\^&\*_]{12,}$", ErrorMessage = "Invalid Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string Code { get; set; }

        }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                };
                return Page();
            }
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

            if ((DateTime.Now - user.LastChanged).TotalMinutes <= 1 )
            {
                ModelState.AddModelError(string.Empty, "Please wait for a while before changing a new password");
                return Page();
            }

            string currentPassword = user.PasswordHash;
            if (!await _passwordService.CheckPasswordHistory(Input.Password, currentPassword, user.LastPassword1, user.LastPassword2))
            {
                ModelState.AddModelError(string.Empty, "Please use another password that is not your last 3 password");
                return Page();
            }
            
            
            var resetResult = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (resetResult.Succeeded)
            {
                user.LastPassword2 = user.LastPassword1;
                user.LastPassword1 = currentPassword;
                user.LastChanged = DateTime.Now;
                var updateResult = await _userManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    await _auditLogService.AddPasswordResetLog(user);
                    return RedirectToPage("./ResetPasswordConfirmation");
                }
                ModelState.AddModelError(string.Empty, "Reset Error. Please re-login and try again");
                return Page();
            }

            foreach (var error in resetResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
