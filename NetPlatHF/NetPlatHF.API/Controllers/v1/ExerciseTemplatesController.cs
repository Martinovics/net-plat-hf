using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetPlatHF.API.Authentication;
using NetPlatHF.BLL.Exceptions;
using NetPlatHF.BLL.Interfaces;
using NetPlatHF.BLL.QueryParamResolvers;
using NetPlatHF.BLL.Services;
using NetPlatHF.DAL.Entities;

namespace NetPlatHF.API.Controllers.v1;




[ApiController]
[Route("api/v{version:apiVersion}/exercise/templates")]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
public class ExerciseTemplatesController : ControllerBase
{
    private readonly IExerciseTemplateService _exerciseTemplateService;
    private readonly IConfiguration _configuration;


    public ExerciseTemplatesController(IExerciseTemplateService exerciseTemplateService, IConfiguration configuration)
    {
        _exerciseTemplateService = exerciseTemplateService;
        _configuration = configuration;
    }




    [MapToApiVersion("1.0")]
    [HttpGet]
    public IEnumerable<ExerciseTemplate> Get([FromQuery] ExerciseTemplateQueryParamResolver resolvedParams)
    {
        return _exerciseTemplateService.GetExerciseTemplates(resolvedParams);
    }

    [MapToApiVersion("1.0")]
    [HttpGet("self")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public IEnumerable<ExerciseTemplate> GetSelf([FromQuery] ExerciseTemplateQueryParamResolver resolvedParams)
    {
        return _exerciseTemplateService.GetUserExerciseTemplates(resolvedParams, FetchApiKey(HttpContext.Request.Headers));
    }


    [MapToApiVersion("1.0")]
    [HttpGet("{id}")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public ActionResult<ExerciseTemplate> GetById(int id)
    {
        try
        {
            return Ok(_exerciseTemplateService.GetUserExerciseTemplate(id, FetchApiKey(HttpContext.Request.Headers)));
        }
        catch (ExerciseTemplateNotFoundException)
        {
            return NotFound();
        }
    }

    /*
    post create exercise
    put update exercise
    delete exercise
    */


    [HttpPost]
    [MapToApiVersion("1.0")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public ActionResult<ExerciseTemplate> Post([FromBody] ExerciseTemplate exerciseTemplate)
    {
        var inserted = _exerciseTemplateService.InsertUserExerciseTemplate(exerciseTemplate, FetchApiKey(HttpContext.Request.Headers));
        return CreatedAtAction(nameof(GetById), new {id = inserted.Id}, inserted);
    }



    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public ActionResult Delete(int id)
    {
        var success = _exerciseTemplateService.DeleteUserExerciseTemplate(id, FetchApiKey(HttpContext.Request.Headers));
        if (success)
        {
            return NoContent();
        }
        return BadRequest();
    }



    [HttpPut("{id}")]
    [MapToApiVersion("1.0")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public ActionResult HttpPut(int id, [FromBody] ExerciseTemplate updatedExerciseTemplate)
    {
        var success = _exerciseTemplateService.UpdateUserExerciseTemplate(id, updatedExerciseTemplate, FetchApiKey(HttpContext.Request.Headers));
        if (success)
        {
            return NoContent();
        }
        return BadRequest();
    }





    private string FetchApiKey(IHeaderDictionary headers)
    {
        string apiKeyName = _configuration.GetValue<string>("Auth:ApiKeyName")!;
        headers.TryGetValue(apiKeyName, out var providedKey);
        return providedKey!.ToString();  // nem lehet null a filter miatt
    }
}
