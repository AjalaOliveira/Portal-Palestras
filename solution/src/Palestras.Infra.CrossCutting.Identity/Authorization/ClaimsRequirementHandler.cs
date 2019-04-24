using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Palestras.Infra.CrossCutting.Identity.Authorization
{
    public class ClaimsRequirementHandler : AuthorizationHandler<ClaimRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ClaimRequirement requirement)
        {
            var claims = context.User.Claims.Where(c => c.Type == requirement.ClaimName);

            foreach (var claim in claims)
                if (claim.Value.Contains(requirement.ClaimValue))
                    context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}