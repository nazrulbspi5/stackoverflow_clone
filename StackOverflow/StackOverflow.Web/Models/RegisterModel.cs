using Autofac;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
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
        public string ReturnUrl { get; set; }
        public IFormFile? Image { get; set; }
        public string Token { get; set; }

        public IList<AuthenticationScheme>? ExternalLogins { get; set; }


        public RegisterModel()
        {
        }

        public RegisterModel(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
        }

        //public async Task<IdentityResult> CreateUserAsync()
        //{
        //    //var user = new UserRegistration
        //    //{
        //    //    FirstName = FirstName,
        //    //    LastName = LastName,
        //    //    Email = Email,
        //    //    Password = Password
        //    //};
        //    //return await _accountService.RegisterUserAsync(user);
        //}

    }
}
