using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MultiIdentityTest.Controllers
{
    [Authorize]
    public class SecuredController : Controller
    {
        [Authorize(AuthenticationSchemes = "AuthSchemeFuffaUno")]
        public IActionResult GoogleSecured() => View();

        [Authorize(AuthenticationSchemes = "AuthSchemeFuffaDue")]
        public IActionResult KleosSecured() => View();

        public IActionResult Generic() => View();
    }
}
