using ECommerceApi.Application.Queries;
using ECommerceApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerceApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserQuery _userQuery;

        public AccountController(IUserQuery userQuery)
        {
            _userQuery = userQuery;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel loginModel)
        {
            if (await _userQuery.CanUserLogin(loginModel.UserName, loginModel.Password))
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, loginModel.UserName));

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                //Just redirect to our index after logging in.
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}