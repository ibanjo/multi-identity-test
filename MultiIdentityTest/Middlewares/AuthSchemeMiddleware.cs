using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using MultiIdentityTest.Helpers;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MultiIdentityTest.Middlewares
{
    public class AuthSchemeMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthSchemeMiddleware (RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext ctx)
        {
            NameValueCollection queryValues = HttpUtility.ParseQueryString(ctx.Request.QueryString.Value);
            bool isContentIntegration = !string.IsNullOrEmpty(queryValues.Get("idp"));
            string requiredIdp = queryValues.Get("idp") ?? "kleos";

            string challengeScheme = MyHelpers.ProviderNameToChallengeScheme(requiredIdp);
            string signInScheme = isContentIntegration
                ? MyAuthenticationSchemes.CiScheme
                : MyAuthenticationSchemes.RegularScheme;

            var userResult = await ctx.AuthenticateAsync(signInScheme);
            bool isAuthenticatd = userResult.Succeeded && userResult.Principal.Identity.IsAuthenticated;

            string idpClaim = userResult.Principal.Claims.FirstOrDefault(c => c.Type == "kleosIdp")?.Value;
            bool challengeSchemesMatch = idpClaim?.Equals(challengeScheme) == true;

            if (isAuthenticatd && (!isContentIntegration || challengeSchemesMatch))
            {
                await _next(ctx);
            }
            else
            {
                await ctx.ChallengeAsync(challengeScheme);
            }
        }
    }
}
