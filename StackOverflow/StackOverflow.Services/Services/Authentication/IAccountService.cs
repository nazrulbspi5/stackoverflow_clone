using Microsoft.AspNetCore.Identity;
using StackOverflow.DAL.Entities.Authentication;
using ApplicationUserBO = StackOverflow.Services.BusinessObjects.Authentication.ApplicationUser;

namespace StackOverflow.Services.Services.Authentication;

public interface IAccountService
{
    Task<IdentityResult> CreateUserAsync(ApplicationUserBO user);
    Task<ApplicationUser> GetUserByEmailAsync(string email);
    Task<ApplicationUser> GetUserAsync();
    Task<SignInResult> PasswordSignInAsync(ApplicationUserBO user);
   
    Task SignInAsync(string email);
    Task SignOutAsync();
    bool IsAuthenticated();
    string GetUserId();
}
