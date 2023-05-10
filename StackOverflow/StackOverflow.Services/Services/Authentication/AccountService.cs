using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using NHibernate;
using StackOverflow.DAL.Entities.Authentication;
using System.Security.Claims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using ApplicationUserBO = StackOverflow.Services.BusinessObjects.Authentication.ApplicationUser;

namespace StackOverflow.Services.Services.Authentication;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _userRoleManager;
    private ISession _session;
    private readonly IActionContextAccessor _contextAccessor;
    private readonly IMapper _mapper;

    public AccountService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<ApplicationRole> userRoleManager,
        IUrlHelperFactory urlHelperFactory,
        IActionContextAccessor contextAccessor,
        IMapper mapper, ISession session)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userRoleManager = userRoleManager;
        _contextAccessor = contextAccessor;
        _mapper = mapper;
        _session = session;
    }

    public async Task<IdentityResult> CreateUserAsync(ApplicationUserBO user)
    {
        var userEntity = _mapper.Map<ApplicationUser>(user);
        var result = await _userManager.CreateAsync(userEntity, user.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRolesAsync(userEntity, new string[] { "User" });
        }
        return result;
    }

    public async Task<ApplicationUser> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<ApplicationUser> GetUserAsync()
    {
        return await _userManager.FindByIdAsync(GetUserId());
    }

    public string GetUserId()
    {
        return GetUser!.FindFirstValue(ClaimTypes.NameIdentifier);
    }
    public ClaimsPrincipal GetUser => _contextAccessor.ActionContext!.HttpContext.User;

    public bool IsAuthenticated()
    {
        return GetUser!.Identity!.IsAuthenticated;
    }

    public async Task<SignInResult> PasswordSignInAsync(ApplicationUserBO user)
    {
        return await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe,
            lockoutOnFailure: false);
    }

    public async Task SignInAsync(string email)
    {
        await _signInManager.SignInAsync(await _userManager.FindByEmailAsync(email), isPersistent: false);
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
