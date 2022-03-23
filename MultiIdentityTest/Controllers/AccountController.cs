using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiIdentityTest.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MultiIdentityTest.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password, string returnUrl)
        {
            if(username == "fuffa" && password == "fuffa")
            {
                var claims = new List<Claim> {
                    new Claim("username", username),
                    new Claim(ClaimTypes.NameIdentifier, username),
                    new Claim(ClaimTypes.Name, "Mr Fuffo de Fuffis")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                var props = new AuthenticationProperties(new Dictionary<string, string> { { ".AuthScheme", MyAuthenticationSchemes.RegularScheme } });
                HttpContext.SignInAsync(MyAuthenticationSchemes.CiScheme, claimsPrincipal, props);
                return Redirect(returnUrl ?? "/");
            }

            TempData["Error"] = "Invalid credentials";
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet("[controller]/[action]/{provider}")]
        public IActionResult LoginExternal([FromRoute] string provider, [FromQuery] string returnUrl)
        {
            if(User?.Identities.Any(id => id.IsAuthenticated) == true)
            {
                return RedirectToAction("Index", "Home");
            } else
            {
                returnUrl = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;
                var authenticationProps = new AuthenticationProperties { RedirectUri = returnUrl };
                var challengeScheme = MyHelpers.ProviderNameToChallengeScheme(provider);
                return new ChallengeResult(challengeScheme, authenticationProps);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Logout()
        {
            string scheme = User.Claims.FirstOrDefault(c => c.Type == ".AuthScheme")?.Value;
            return new SignOutResult(new[] { MyAuthenticationSchemes.RegularScheme, scheme });
        }

        [Authorize]
        public IActionResult Denied()
        {
            return View();
        }
    }
}
