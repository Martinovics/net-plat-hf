using Microsoft.AspNetCore.Mvc;
using NetPlatHF.API.Authentication;
using NetPlatHF.BLL.Dtos;
using NetPlatHF.BLL.Exceptions;
using NetPlatHF.BLL.Interfaces;


namespace NetPlatHF.API.Controllers.v1;




[Route("api/v{version:apiVersion}/template")]
[ApiController]
[ApiVersion("1.0")]
public class TemplateController : ControllerBase
{
    private readonly ITemplateService _templateService;
    private readonly IConfiguration _configuration;


    public TemplateController(ITemplateService templateService, IConfiguration configuration)
    {
        _templateService = templateService;
        _configuration = configuration;
    }




    /// <summary>
    /// Kilistazza a publikus csoportokat, gyakorlatokkal egyutt
    /// </summary>
    /// <returns>Publikus csoportok gyakorlatokkal</returns>
    /// <response code="200">Sikeres listazas</response>
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<BLL.Dtos.Template> List()
    {
        return _templateService.List();
    }




    /// <summary>
    /// Kilistazza egy felhasznalo csoportjait, gyakorlatokkal egyutt
    /// </summary>
    /// <returns>Csoportok gyakorlatokkal</returns>
    /// <response code="200">Sikeres listazas</response>
    [HttpGet("self")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public IEnumerable<BLL.Dtos.Template> ListSelf()
    {
        return _templateService.ListSelf(FetchApiKey(HttpContext));
    }




    /// <summary>
    /// Visszaadja a felhasznalo egy csoportjat, gyakorlatokkal egyutt a megadott azonosito alapjan
    /// </summary>
    /// <returns>Csoport gyakorlatokkal</returns>
    /// <response code="200">Sikeres listazas</response>
    [HttpGet("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public IEnumerable<BLL.Dtos.Template> Get(int id)
    {
        throw new NotImplementedException();
    }




    /// <summary>
    /// Beleteszi a gyakorlatot a csoportba
    /// </summary>
    /// <param name="groupId">Csoport azonositoja, amibe felvesszuk a gyakorlatot</param>
    /// <param name="exerciseId">Gyakorlat azonositoja</param>
    /// <param name="createTemplate">Extra informacio a gyakorlathoz</param>
    /// <returns>Letrehozott gyakorlat-csoport osszerendeles</returns>
    /// <response code="201">Sikeres osszerendeles</response>
    /// <response code="400">Sikertelen osszerendeles</response>
    [HttpPost("group/{groupId}/exercise/{exerciseId}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public ActionResult<BLL.Dtos.Template> Create(int groupId, int exerciseId, [FromBody] CreateTemplate createTemplate)
    {
        try
        {
            var template = _templateService.Create(groupId, exerciseId, createTemplate, FetchApiKey(HttpContext));
            return CreatedAtAction(nameof(Get), new { id = template.Id }, template);
        }
        catch (Exception ex) when (ex is ArgumentException || ex is InvalidOwnerException || ex is GroupTemplateNotFoundException || ex is ExerciseTemplateNotFoundException)
        {
            ModelState.AddModelError(nameof(Create), ex.Message);
            return ValidationProblem(ModelState);
        }
    }




    /// <summary>
    /// Kiveszi a gyakorlatot a csoportbol
    /// </summary>
    /// <param name="id">Az osszerendeles azonositoja, amit torolni szeretnenk</param>
    /// <returns>No content</returns>
    /// <response code="200">Sikeres torles</response>
    /// <response code="404">Sikertelen torles</response>
    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public ActionResult<BLL.Dtos.Template> Delete(int id)
    {
        var template = _templateService.Delete(id, FetchApiKey(HttpContext));
        return template != null ? NoContent() : NotFound();
    }




    private string FetchApiKey(HttpContext httpContext)  // TODO make it static | extract
    {
        string apiKeyName = _configuration.GetValue<string>("Auth:ApiKeyName")!;
        httpContext.Request.Headers.TryGetValue(apiKeyName, out var providedKey);
        return providedKey!.ToString();  // nem lehet null a filter miatt
    }
}
