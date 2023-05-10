using Autofac;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using StackOverflow.Services.BusinessObjects.Authentication;
using StackOverflow.Services.Services.Authentication;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Models
{
    public class LoginModel : BaseModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string Token { get; set; }
        public IList<AuthenticationScheme>? ExternalLogins { get; set; }
        public string ReturnUrl { get; set; }
        private IAccountService _accountService;
        public LoginModel()
        {
        }

        public LoginModel(IAccountService accountService)
            : base()
        {
            _accountService = accountService;
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _accountService = scope.Resolve<IAccountService>();
        }


        public async Task<SignInResult> PasswordSignInAsync()
        {
            var user = new ApplicationUser
            {
               
                UserName = Email,
                Password = Password,
                RememberMe = RememberMe
            };

            return await _accountService.PasswordSignInAsync(user);
        }

        //public async Task LogoutUserAsync()
        //{
        //    await _accountService.UserLogoutAsync();
        //}

    }
}
