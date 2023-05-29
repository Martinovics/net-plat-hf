using Microsoft.AspNetCore.Identity;
using NetPlatHF.BLL.Interfaces;
using NetPlatHF.DAL.Data;

namespace NetPlatHF.BLL.Services;




public class ApiKeyService : IApiKeyService  // TODO ide is siman at lehet adni a db contextet
{
    private readonly UserManager<AppUser> _userManager;


    public ApiKeyService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }


    public bool ApiKeyExists(string apiKey)
    {
        if (!ApiKeyValid(apiKey))
            return false;

        AppUser? apiKeys = _userManager.Users.SingleOrDefault(x => x.ApiKey == apiKey);
        return apiKeys != null;
    }


    private bool ApiKeyValid(string apiKey)
    {
        if (apiKey.Length != 32)
            return false;

        return true;
    }

}
