using Microsoft.AspNetCore.Mvc;

namespace StackOverflow.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
