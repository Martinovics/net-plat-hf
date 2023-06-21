using Microsoft.AspNetCore.Mvc;
using NetPlatHF.API.Authentication;
using NetPlatHF.BLL.Dtos;
using NetPlatHF.BLL.Interfaces;


namespace NetPlatHF.API.Controllers.v2;




[ApiController]
[Route("api/v{version:apiVersion}/group/templates")]
[ApiVersion("2.0")]
public class GroupTemplatesController : ControllerBase
{
    private readonly IGroupTemplateService _groupTemplateService;
    private readonly IConfiguration _configuration;


    public GroupTemplatesController(IGroupTemplateService groupTemplateService, IConfiguration configuration)
    {
        _groupTemplateService = groupTemplateService;
        _configuration = configuration;
    }




    /// <summary>
    /// Letrehoz egy csoportot es hozzaadja a megadott gyakorlatokat is
    /// </summary>
    /// <param name="newGroup">Letrehozando csoport es gyakorlatok</param>
    /// <returns>Letrehozott csoport es gyakorlatok</returns>
    /// <response code="201">Sikeres letrehozas</response>
    /// <response code="400">Sikertelen letrehozas</response>
    [HttpPost]
    [MapToApiVersion("2.0")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public ActionResult<BLL.Dtos.GroupTemplate> Create([FromBody] CreateGroupBulkedExercise newGroup)
    {
        try
        {
            var created = _groupTemplateService.InsertGroupBulkedExercises(newGroup, FetchApiKey(HttpContext));
            return CreatedAtAction(nameof(v1.GroupTemplatesController.Get), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(nameof(CreateGroupTemplate.Name), ex.Message);
            return ValidationProblem(ModelState);
        }
    }




    private string FetchApiKey(HttpContext httpContext)  // TODO make it static | extract
    {
        string apiKeyName = _configuration.GetValue<string>("Auth:ApiKeyName")!;
        httpContext.Request.Headers.TryGetValue(apiKeyName, out var providedKey);
        return providedKey!.ToString();  // nem lehet null a filter miatt
    }
}
