using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UILayer.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("Login");
        }
        [Authorize]
        public IActionResult Login()
        {
            return View();
        }
    }


}
