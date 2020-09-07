using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace NetCoreRefresher.Authorization
{
    public class AdminApiKeyAuthorizationHandler : AuthorizationHandler<AdminApiKeyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminApiKeyRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}