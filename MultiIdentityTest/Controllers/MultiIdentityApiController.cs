using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiIdentityTest.Dtos;

namespace MultiIdentityTest.Controllers
{
    [Route("api/multi-identity")]
    [Authorize("kleosOps")]
    [ApiController]
    public class MultiIdentityApiController : Controller
    {
        [HttpGet("simple-value")]
        public IActionResult SimpleValue() => new JsonResult(new ResponseDto
        {
            StringValue = "test",
            NumericValue = 42,
            NestedValue = new NestedDto
            {
                NestedStringValue = "nested-test",
                NestedNumericValue = 43
            }
        });
    }
}
