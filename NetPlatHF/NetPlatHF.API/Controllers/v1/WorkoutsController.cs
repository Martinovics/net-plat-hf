using Microsoft.AspNetCore.Mvc;
using NetPlatHF.API.Authentication;
using NetPlatHF.BLL.Dtos;
using NetPlatHF.BLL.Exceptions;
using NetPlatHF.BLL.Interfaces;


namespace NetPlatHF.API.Controllers.v1;




[ApiController]
[Route("api/v{version:apiVersion}/workout")]
[ApiVersion("1.0")]
public class WorkoutsController : ControllerBase
{
    private readonly IWorkoutService _workoutService;
    private readonly IConfiguration _configuration;


    public WorkoutsController(IWorkoutService workoutService, IConfiguration configuration)
    {
        _workoutService = workoutService;
        _configuration = configuration;
    }




    /// <summary>
    /// Kilistazza a publikusan elerheto edzeseket
    /// </summary>
    /// <returns>Publikus gyakorlatok listaja</returns>
    /// <response code="200">Sikeres listazas</response>
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<BLL.Dtos.Workout> List()
    {
        return _workoutService.List();
    }




    /// <summary>
    /// Kilistazza a felhasznalohoz tartozo edzeseket
    /// </summary>
    /// <returns>Gyakorlatok listaja</returns>
    /// <response code="200">Sikeres listazas</response>
    [HttpGet("self")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public IEnumerable<BLL.Dtos.Workout> ListSelf()
    {
        var apiKey = FetchApiKey(HttpContext);
        return _workoutService.ListSelf(apiKey);
    }




    /// <summary>
    /// Visszaad egy edzest a neve alapjan
    /// </summary>
    /// <param name="name">Edzes neve</param>
    /// <returns>Edzes</returns>
    /// <response code="200">Edzes megtalalva</response>
    /// <response code="404">Edzes nem talalhato</response>
    [HttpGet("{name}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<BLL.Dtos.Workout> Get(string name)
    {
        string? apiKey = null;
        try
        {
            apiKey = FetchApiKey(HttpContext);
        }
        catch (Exception) { }

        var template = _workoutService.GetByName(name, apiKey);
        return template != null ? Ok(template) : NotFound();
    }




    /// <summary>
    /// Letrehoz egy edzest
    /// </summary>
    /// <param name="name">Edzes neve</param>
    /// <returns>Letrejott edzes</returns>
    /// <response code="201">Edzes letrejott</response>
    /// <response code="400">Edzes nem jott letre</response>
    [HttpPost("{name}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public ActionResult<BLL.Dtos.Template> Create(string name)
    {
        try
        {
            var workout = _workoutService.Create(new CreateWorkout(name), FetchApiKey(HttpContext));
            return CreatedAtAction(nameof(Get), new { name = workout.Name }, workout);
        }
        catch (Exception ex) when (ex is ArgumentException || ex is InvalidOwnerException || ex is DuplicateWorkoutException)
        {
            ModelState.AddModelError(nameof(Create), ex.Message);
            return ValidationProblem(ModelState);
        }
    }




    /// <summary>
    /// Hozzaad egy csoportot az edzeshez
    /// </summary>
    /// <param name="name">Edzes neve</param>
    /// <param name="groupId">Csoport azonositoja</param>
    /// <returns>Ok</returns>
    /// <response code="200">A csoport hozzaadodott az edzeshez</response>
    /// <response code="404">A csoport nem adodott hozza az edzeshez</response>
    [HttpPost("{name}/group/{groupId}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public ActionResult<BLL.Dtos.Template> Add(string name, int groupId)
    {
        var workout = _workoutService.Add(name, groupId, FetchApiKey(HttpContext));
        return workout != null ? Ok(workout) : NotFound();
    }




    /// <summary>
    /// Torol egy csoportot az edzesbol
    /// </summary>
    /// <param name="name">Edzes neve</param>
    /// <param name="groupId">Csoport azonositoja</param>
    /// <returns>Ok</returns>
    /// <response code="200">A csoport torlodott az edzesbol</response>
    /// <response code="404">A csoport nem torlodott az edzesbol</response>
    [HttpDelete("{name}/group/{groupId}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public ActionResult<BLL.Dtos.Template> Remove(string name, int groupId)
    {
        var workout = _workoutService.Remove(name, groupId, FetchApiKey(HttpContext));
        return workout != null ? Ok(workout) : NotFound();
    }




    /// <summary>
    /// Torol egy edzest
    /// </summary>
    /// <param name="name">Torlendo edzes neve</param>
    /// <returns>Ok</returns>
    /// <response code="200">Az edzes torlodott</response>
    /// <response code="404">Az edzes nem torlodott</response>
    [HttpDelete("{name}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public ActionResult<BLL.Dtos.Template> Delete(string name)
    {
        var workout = _workoutService.Delete(name, FetchApiKey(HttpContext));
        return workout != null ? NoContent() : NotFound();
    }




    private string FetchApiKey(HttpContext httpContext)  // TODO make it static | extract
    {
        string apiKeyName = _configuration.GetValue<string>("Auth:ApiKeyName")!;
        httpContext.Request.Headers.TryGetValue(apiKeyName, out var providedKey);
        return providedKey!.ToString();  // nem lehet null a filter miatt
    }
}
