using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCoreRefresher.Config;

namespace NetCoreRefresher.Authorization
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private const string ApiKeyHeaderName = "AuthorizationCode";

        private ApiKeys _apiKeys;
        
        public ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeyAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IOptions<ApiKeys> settings) 
            : base(options, logger, encoder, clock)
        {
            _apiKeys = settings.Value;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var authenticationKeys))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            if (authenticationKeys.Count == 0)
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var key = authenticationKeys.First();

            if (_apiKeys.Keys.Any(x => x == key) || _apiKeys.AdminKeys.Any(x => x == key))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "ValidApiUser")
                };

                if (_apiKeys.AdminKeys.Any(x => x == key))
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }
                
                var identity = new ClaimsIdentity(claims, Options.AuthenticationType);
                var identities = new List<ClaimsIdentity> {identity};
                var principal = new ClaimsPrincipal(identities);
                var ticket = new AuthenticationTicket(principal, Options.Scheme);
                
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }

            return Task.FromResult(AuthenticateResult.Fail("Invalid authorization key"));
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 401;

            await Response.WriteAsync("Invalid api key");
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 403;

            await Response.WriteAsync("Invalid api key");
        }
    }
}