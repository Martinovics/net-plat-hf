using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using NetPlatHF.BLL.Interfaces;
using System.Diagnostics;

namespace NetPlatHF.API.Authentication;




public class ApiKeyAuthFilter : IAuthorizationFilter
{
    private readonly IConfiguration _configuration;
    private readonly IApiKeyService _apiKeyService;


    public ApiKeyAuthFilter(IConfiguration configuration, IApiKeyService apiKeyService)
    {
        _configuration= configuration;
        _apiKeyService = apiKeyService;
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
        if (!_apiKeyService.ApiKeyExists(providedKey.ToString()))
        {
            context.Result = new UnauthorizedObjectResult($"Invalid api key.");
            return;
        }
    }
}
