using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyFinanceBlazor.UI;
using MyFinanceBlazor.UI.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAntDesign();
builder.Services.Configure<RelatedThings>(options => builder.Configuration.GetSection("RelatedThings").Bind(options));

builder.Services.AddScoped<CustomAuthorizationMessageHandler>();

var baseAddress = builder.Configuration["RelatedThings:AuthorizedUrls"];

builder.Services.AddHttpClient("MyFinance.ServerAPI", client =>
client.BaseAddress = new Uri(baseAddress))
    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("MyFinance.ServerAPI"));

var scopes = builder.Configuration["RelatedThings:ScopeUrl"];

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add(scopes);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");
});

await builder.Build().RunAsync();
