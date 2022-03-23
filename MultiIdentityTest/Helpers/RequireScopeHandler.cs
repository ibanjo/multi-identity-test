using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MultiIdentityTest.Helpers
{
    public class ScopeRequirement : IAuthorizationRequirement
    {
        public string Scope { get; }

        public ScopeRequirement(string scope)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }
    }

    public class RequireScopeHandler : AuthorizationHandler<ScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ScopeRequirement requirement)
        {
            Claim theClaim = context.User.FindFirst(x => x.Type == "scope" && x.Value == requirement.Scope);

            if (theClaim != null)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}