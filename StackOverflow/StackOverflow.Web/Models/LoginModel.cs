using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Autofac;

namespace DevTrack.Web.Models
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

        public LoginModel()
        {
        }

        public LoginModel(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
        }
       

        //public Task<(SignInResult Result, Status Status)> LoginUserAsync()
        //{
        //    return _accountService.UserLoginAsync(Email, Password, RememberMe);
        //}

        //public async Task LogoutUserAsync()
        //{
        //    await _accountService.UserLogoutAsync();
        //}
       
    }
}
