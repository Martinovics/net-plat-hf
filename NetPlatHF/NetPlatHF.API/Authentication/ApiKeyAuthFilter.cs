using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace NetPlatHF.API.Authentication;




public class ApiKeyAuthFilter : IAuthorizationFilter
{
    private readonly IConfiguration _configuration;


    public ApiKeyAuthFilter(IConfiguration configuration)
    {
        _configuration= configuration;
    }


    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string apiKeyName = _configuration.GetValue<string>("Auth:ApiKeyName")!;
        
        // ha nincs megadva kulcs
        if (!context.HttpContext.Request.Headers.TryGetValue(apiKeyName, out var providedKey))
        {
            context.Result = new UnauthorizedObjectResult($"Api key missing. Check header for {apiKeyName}");
            return;
        }

        // ha nincs ilyen kulcs
        if (providedKey.Equals("asd"))
        {
            context.Result = new UnauthorizedObjectResult($"Invalid api key.");
            return;
        }
    }
}
