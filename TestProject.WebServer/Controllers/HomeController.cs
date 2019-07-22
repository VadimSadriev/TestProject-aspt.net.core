using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Core;
using TestProject.WebServer.Models;

namespace TestProject.WebServer.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<AppUser> userManager;
        private DataContext context;

        public HomeController(UserManager<AppUser> userManager, DataContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        [Authorize(Roles = "Admin,CanViewRequest")]
        public IActionResult PageList()
        {
            return PartialView();
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
