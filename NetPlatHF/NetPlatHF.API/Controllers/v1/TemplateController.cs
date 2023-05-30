using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using NetPlatHF.API.Authentication;
using NetPlatHF.BLL.Dtos;
using NetPlatHF.BLL.Exceptions;
using NetPlatHF.BLL.Interfaces;
using NetPlatHF.BLL.Services;

namespace NetPlatHF.API.Controllers.v1;




[Route("api/v{version:apiVersion}/template")]
[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
public class TemplateController : ControllerBase
{
    private readonly ITemplateService _templateService;
    private readonly IConfiguration _configuration;


    public TemplateController(ITemplateService templateService, IConfiguration configuration)
    {
        _templateService = templateService;
        _configuration = configuration;
    }




    [MapToApiVersion("1.0")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<BLL.Dtos.Template> List()
    {
        return _templateService.List();
    }




    [MapToApiVersion("1.0")]
    [HttpGet("self")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public IEnumerable<BLL.Dtos.Template> ListSelf()
    {
        return _templateService.ListSelf(FetchApiKey(HttpContext));
    }




    [MapToApiVersion("1.0")]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public IEnumerable<BLL.Dtos.Template> Get(int id)
    {
        throw new NotImplementedException();
    }




    [MapToApiVersion("1.0")]
    [HttpPost("group/{groupId}/exercise/{exerciseId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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




    private string FetchApiKey(HttpContext httpContext)  // TODO make it static | extract
    {
        string apiKeyName = _configuration.GetValue<string>("Auth:ApiKeyName")!;
        httpContext.Request.Headers.TryGetValue(apiKeyName, out var providedKey);
        return providedKey!.ToString();  // nem lehet null a filter miatt
    }
}
