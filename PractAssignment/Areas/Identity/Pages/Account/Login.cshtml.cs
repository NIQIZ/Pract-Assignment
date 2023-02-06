// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using PractAssignment.Models;
using PractAssignment.Services;
using PractAssignment.ViewModels;

namespace PractAssignment.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly GoogleCaptchaService _googleCaptchaService;
        private readonly IEmailSender _emailSender;

        public LoginModel(SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager, 
            ILogger<LoginModel> logger, 
            GoogleCaptchaService googleCaptchaService , 
            IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _googleCaptchaService = googleCaptchaService;
            _emailSender = emailSender;

        }
        [TempData]
        public string StatusMessage { get; set; }
        
        [BindProperty]
        public LoginView Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        
        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var captchaResult = await _googleCaptchaService.VerifyToken(Input.Token);
                Console.WriteLine("Captcha Result: " + captchaResult);
                if (!captchaResult)
                {
                    return Page();
                }

                var existingUser = await _userManager.FindByEmailAsync(Input.Email);

                if (existingUser != null)
                {
                    var verificationResult =
                        _userManager.PasswordHasher.VerifyHashedPassword(existingUser, existingUser.PasswordHash,
                            Input.Password);
                    if (((DateTime.Now - existingUser.LastChanged).TotalMinutes >= 3 ) && (verificationResult == PasswordVerificationResult.Success))
                    {
                        var code = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ResetPassword",
                            pageHandler: null,
                            values: new { area = "Identity", code },
                            protocol: Request.Scheme);
                        
                        await _emailSender.SendEmailAsync(
                            Input.Email,
                            "Reset Password",
                            $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                        
                        return RedirectToPage("./ExpiredPasswordConfirmation");

                    }
                }
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    
                    //Create the security context
                    var claims = new List<Claim> {
                        new Claim("Testing", "Work Please"),
                    };
                    
                    var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
