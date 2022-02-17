using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MultiIdentityTest.Controllers
{
    [Authorize]
    public class SecuredController : Controller
    {
        [Authorize(AuthenticationSchemes = "AuthSchemeFuffaUno")]
        public async Task<IActionResult> GoogleSecured()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = "AuthSchemeFuffaDue")]
        public async Task<IActionResult> KleosSecured()
        {
            return View();
        }

        public IActionResult Generic()
        {
            return View();
        }
    }
}
