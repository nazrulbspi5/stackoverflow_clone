using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.DAL.Entities.Authentication;
using StackOverflow.Services.Services.Authentication;
using StackOverflow.Web.Models;

namespace StackOverflow.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountService _accountService;
        private readonly ILifetimeScope _scope;
        private readonly ILogger<AccountController> _logger;
        public AccountController(ILogger<AccountController> logger, ILifetimeScope scope,
            SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            IAccountService accountService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _scope = scope;
            _accountService = accountService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Register(string returnUrl = null!)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            var model = _scope.Resolve<RegisterModel>();
            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            model.ReturnUrl ??= Url.Content("~/");

            try
            {
                if (ModelState.IsValid)
                {
                    model.ResolveDependency(_scope);
                    var result = await model.CreateUserAsync();

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            //ViewResponse(error.Description, ResponseTypes.Danger);
                        }
                        return View(model);
                    }

                    ModelState.Clear();

                    await model.SignInAsync();
                    model.ReturnUrl ??= Url.Content("~/Home");
                    _logger.LogInformation("User created a new account with password.");
                    return LocalRedirect(model.ReturnUrl!);
                }
            }
            catch (Exception ex)
            {
               // ViewResponse(ex.Message, ResponseTypes.Danger);
                _logger.LogError(ex, ex.Message);
            }

            return View(model);
        }

        public IActionResult Login(string returnUrl = null!)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            var model = _scope.Resolve<LoginModel>();

            try
            {
                returnUrl ??= Url.Content("~/");

                model.ReturnUrl = returnUrl;
            }
            catch (Exception ex)
            {
               // ViewResponse(ex.Message, ResponseTypes.Danger);
                _logger.LogError(ex, ex.Message);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.ResolveDependency(_scope);
                    var result = await model.PasswordSignInAsync();
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        //ViewResponse("Invalid login attempt.", ResponseTypes.Danger);
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                //ViewResponse(ex.Message, ResponseTypes.Danger);
                _logger.LogError(ex, ex.Message);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string returnUrl = null!)
        {
            try
            {
                //var model = _scope.Resolve<LogoutModel>();
               // await model.SignOutAsync();
            }
            catch (Exception ex)
            {
                //ViewResponse(ex.Message, ResponseTypes.Danger);
                _logger.LogError(ex, ex.Message);
            }

            return RedirectToAction("Login");
        }
    }
}