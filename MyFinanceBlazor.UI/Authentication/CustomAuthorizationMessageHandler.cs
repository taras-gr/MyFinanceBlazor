using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;

namespace MyFinanceBlazor.UI.Authentication
{
    public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        private readonly IOptions<RelatedThings> _options;

        public CustomAuthorizationMessageHandler(IAccessTokenProvider provider, IOptions<RelatedThings> options,
            NavigationManager navigationManager)
            : base(provider, navigationManager)
        {
            _options = options;

            ConfigureHandler(
                authorizedUrls: new[] { _options.Value.AuthorizedUrls },
                scopes: new[] { _options.Value.ScopeUrl });
        }
    }

    public class RelatedThings
    {
        public string AuthorizedUrls { get; set; }

        public string ScopeUrl { get; set; }
    }
}
