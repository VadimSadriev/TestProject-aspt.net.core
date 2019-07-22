using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestProject.Core;

namespace TestProject.WebServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IRepository repository;

        public AccountController(
                UserManager<AppUser> userManager,
                SignInManager<AppUser> signInManager,
                IRepository repository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["returnUrl"] = returnUrl;
            ViewData["Title"] = "Вход в учётную запись";
            return View();
        }

        [HttpPost]
        public async Task<ApiResponse<LoginResultVm>> Login([FromBody]LoginVm loginVm, string returnUrl = null)
        {
            var defaultErrorMessage = "Некорректный логин или пароль";

            var defaultErrorResponse = new ApiResponse<LoginResultVm>
            {
                ErrorMessage = defaultErrorMessage
            };

            if (loginVm == null || loginVm.UserNameOrEmail == null || string.IsNullOrWhiteSpace(loginVm.Password))
            {
                return defaultErrorResponse;
            }

            var user = loginVm.UserNameOrEmail.IsEmail()
                       ? await userManager.FindByEmailAsync(loginVm.UserNameOrEmail)
                       : await userManager.FindByNameAsync(loginVm.UserNameOrEmail);

            if (user == null)
            {
                return defaultErrorResponse;
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, loginVm.Password, loginVm.RememberMe, false);

            if (signInResult.Succeeded)
            {
                return new ApiResponse<LoginResultVm>
                {

                    Response = new LoginResultVm
                    {
                        ReturnUrl = !string.IsNullOrEmpty(returnUrl) && returnUrl.IsValidUrl() ? returnUrl : "/"
                    }
                };
            }

            return defaultErrorResponse;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        [HttpGet]
        [Authorize]
        public async Task<ApiResponse<AppUserVm>> GetCurrentUser()
        {
            var userVm = await repository.AppUsers.GetCurrentUserVmWithPermissions();

            return new ApiResponse<AppUserVm>
            {
                Response = userVm
            };
        }
    }
}
