using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentWebPortfolio.Business.Queries;
using StudentWebPortfolio.Common;
using StudentWebPortfolio.Data.Entities;
using StudentWebPortfolio.Web.Managers;

namespace StudentWebPortfolio.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        #region Ctor
        private readonly SignInManager _signInManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IUserQueries _userQueries;

        public RegisterModel(
            UserManager userManager,
            SignInManager signInManager,
            RoleManager roleManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IUserQueries userQueries)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
            _userQueries = userQueries;
        }
        #endregion

        #region Model
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required, Display(Name = "Username"), RegularExpression("[a-zA-Z0-9_]+", ErrorMessage = "Only letters and numbers are valid.")]
            [StringLength(32, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
            public string Username { get; set; }

            [Required, EmailAddress, Display(Name = "Email")]
            public string Email { get; set; }

            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [Required, DataType(DataType.Password), Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password), Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required, Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required, Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required, Display(Name = "Category")]
            public string Category { get; set; }
        }
        #endregion

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (!UserRole.UserRoles.Any(_ => _ == Input.Category))
            {
                ModelState.AddModelError("Input.Category", "Invalid category.");
                return Page();
            }

            if ((await _userQueries.ByEmailAsync(Input.Email).AnyAsync()))
            {
                ModelState.AddModelError("Input.Email", "This email is already in use by another user.");
                return Page();
            }

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = Input.Username,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                };

                var result = await _userManager.CreateAsync(user, Input.Password, Input.Category);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
