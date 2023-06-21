using Microsoft.AspNetCore.Mvc;
using NetPlatHF.API.Authentication;
using NetPlatHF.BLL.Interfaces;
using NetPlatHF.BLL.Dtos;


namespace NetPlatHF.API.Controllers.v1;




[ApiController]
[Route("api/v{version:apiVersion}/exercise/templates")]
[ApiVersion("1.0")]
public class ExerciseTemplatesController : ControllerBase
{
    private readonly IExerciseTemplateService _exerciseTemplateService;
    private readonly IConfiguration _configuration;


    public ExerciseTemplatesController(IExerciseTemplateService exerciseTemplateService, IConfiguration configuration)
    {
        _exerciseTemplateService = exerciseTemplateService;
        _configuration = configuration;
    }




    /// <summary>
    /// Visszaadja a publikusan elerheto gyakorlatok listajat.
    /// </summary>
    /// <returns>Publikus gyakorlatok listaja</returns>
    /// <response code="200">Sikeres listazas</response>
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<BLL.Dtos.ExerciseTemplate> List()
    {
        return _exerciseTemplateService.ListTemplates();
    }




    /// <summary>
    /// Kilistazza a felhasznalo gyakorlatait
    /// </summary>
    /// <returns>A felhasznalohoz tartozo gyakorlatok listaja</returns>
    /// <response code="200">Sikeres listazas</response>
    [HttpGet("self")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public IEnumerable<BLL.Dtos.ExerciseTemplate> ListSelf()
    {
        var apiKey = FetchApiKey(HttpContext);
        return _exerciseTemplateService.ListUserTemplates(apiKey);
    }




    /// <summary>
    /// Visszadja a felhasznalohoz tartozo gyakorlatot azonosito alapjan
    /// </summary>
    /// <param name="id">A gyakorlat azonositoja</param>
    /// <returns>Gyakorlat</returns>
    /// <response code="200">A gyakorlat megtalalva</response>
    /// <response code="404">A gyakorlat nem talalhato</response>
    [HttpGet("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<BLL.Dtos.ExerciseTemplate> Get(int id)
    {
        string? apiKey = null;
        try
        {
            apiKey = FetchApiKey(HttpContext);
        }
        catch (Exception) { }

        var template = _exerciseTemplateService.GetTemplateById(id, apiKey);
        return template != null ? Ok(template) : NotFound();
    }




    /// <summary>
    /// Letrehoz egy gyakorlatot
    /// </summary>
    /// <param name="newTemplate">Letrehozando gyakorlat</param>
    /// <returns>Letrehozott gyakorlat</returns>
    /// <response code="201">Sikeres letrehozas</response>
    /// <response code="400">Sikertelen letrehozas</response>
    [HttpPost]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public ActionResult<BLL.Dtos.ExerciseTemplate> Create([FromBody] CreateExerciseTemplate newTemplate)
    {
        try
        {
            var created = _exerciseTemplateService.Insert(newTemplate, FetchApiKey(HttpContext));
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(nameof(CreateExerciseTemplate.Name), ex.Message);
            return ValidationProblem(ModelState);
        }
    }




    /// <summary>
    /// Frissit egy gyakorlatot
    /// </summary>
    /// <param name="id">Frissitendo gyakorlat azonositoja</param>
    /// <param name="newTemplate">Uj adatok, amire frissul a gyakorlat</param>
    /// <returns>Frissitett gyakorlat</returns>
    /// <response code="200">Sikeres frissites</response>
    /// <response code="404">Sikertelen frissites</response>
    [HttpPatch("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public ActionResult<BLL.Dtos.ExerciseTemplate> Update(int id, [FromBody] UpdateExerciseTemplate newTemplate)
    {
        var template = _exerciseTemplateService.Update(id, newTemplate, FetchApiKey(HttpContext));
        return template != null ? Ok(template) : NotFound();
    }




    /// <summary>
    /// Torol egy gyakorlatot
    /// </summary>
    /// <param name="id">Torlendo gyakorlat azonositoja</param>
    /// <returns>No content</returns>
    /// <response code="204">Sikeres torles</response>
    /// <response code="404">Sikertelen torles</response>
    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public ActionResult<BLL.Dtos.ExerciseTemplate> Delete(int id)
    {
        var template = _exerciseTemplateService.Delete(id, FetchApiKey(HttpContext));
        return template != null ? NoContent() : NotFound();
    }




    private string FetchApiKey(HttpContext httpContext)
    {
        string apiKeyName = _configuration.GetValue<string>("Auth:ApiKeyName")!;
        httpContext.Request.Headers.TryGetValue(apiKeyName, out var providedKey);
        return providedKey!.ToString();  // nem lehet null a filter miatt
    }
}
