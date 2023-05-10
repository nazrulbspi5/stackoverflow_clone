using Autofac;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using StackOverflow.Services.BusinessObjects.Authentication;
using StackOverflow.Services.Services.Authentication;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Models
{
    public class RegisterModel : BaseModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string? ReturnUrl { get; set; }
        private IAccountService _accountService;
        public IList<AuthenticationScheme>? ExternalLogins { get; set; }


        public RegisterModel()
        {
        }

        public RegisterModel(IAccountService accountService)
            : base()
        {
            _accountService = accountService;
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _accountService = scope.Resolve<IAccountService>();
        }

        public async Task<IdentityResult> CreateUserAsync()
        {
            var user = new ApplicationUser
            {
                FirstName = FirstName,
                LastName = LastName,
                UserName = Email,
                Email = Email,
                Password = Password
            };
            return await _accountService.CreateUserAsync(user);
        }
        public async Task SignInAsync()
        {
            await _accountService.SignInAsync(Email);
        }

    }
}
