using Microsoft.AspNetCore.Authorization;

namespace NetCoreRefresher.Authorization
{
    public class AdminApiKeyRequirement : IAuthorizationRequirement
    {
        public const string AdminApiRequirement = "AdminApiKey";
    }
}