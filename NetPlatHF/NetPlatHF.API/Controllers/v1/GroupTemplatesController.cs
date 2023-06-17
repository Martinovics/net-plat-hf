using Microsoft.AspNetCore.Mvc;
using NetPlatHF.API.Authentication;
using NetPlatHF.BLL.Interfaces;
using NetPlatHF.BLL.Dtos;


namespace NetPlatHF.API.Controllers.v1;




[ApiController]
[Route("api/v{version:apiVersion}/group/templates")]
[ApiVersion("1.0")]
public class GroupTemplatesController : ControllerBase
{
    private readonly IGroupTemplateService _groupTemplateService;
    private readonly IConfiguration _configuration;


    public GroupTemplatesController(IGroupTemplateService groupTemplateService, IConfiguration configuration)
    {
        _groupTemplateService = groupTemplateService;
        _configuration = configuration;
    }




    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<BLL.Dtos.GroupTemplate> List()
    {
        return _groupTemplateService.ListTemplates();
    }




    [HttpGet("self")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public IEnumerable<BLL.Dtos.GroupTemplate> ListSelf()
    {
        var apiKey = FetchApiKey(HttpContext);
        return _groupTemplateService.ListUserTemplates(apiKey);
    }




    [HttpGet("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<BLL.Dtos.GroupTemplate> Get(int id)
    {
        string? apiKey = null;
        try
        {
            apiKey = FetchApiKey(HttpContext);
        }
        catch (Exception) { }

        var template = _groupTemplateService.GetTemplateById(id, apiKey);
        return template != null ? Ok(template) : NotFound();
    }




    [HttpPost]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public ActionResult<BLL.Dtos.GroupTemplate> Create([FromBody] CreateGroupTemplate newTemplate)
    {
        try
        {
            var created = _groupTemplateService.Insert(newTemplate, FetchApiKey(HttpContext));
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(nameof(CreateGroupTemplate.Name), ex.Message);
            return ValidationProblem(ModelState);
        }
    }




    [HttpPatch("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public ActionResult<BLL.Dtos.GroupTemplate> Update(int id, [FromBody] UpdateGroupTemplate newTemplate)
    {
        var template = _groupTemplateService.Update(id, newTemplate, FetchApiKey(HttpContext));
        return template != null ? Ok(template) : NotFound();
    }




    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public ActionResult<BLL.Dtos.GroupTemplate> Delete(int id)
    {
        var template = _groupTemplateService.Delete(id, FetchApiKey(HttpContext));
        return template != null ? NoContent() : NotFound();
    }




    private string FetchApiKey(HttpContext httpContext)
    {
        string apiKeyName = _configuration.GetValue<string>("Auth:ApiKeyName")!;
        httpContext.Request.Headers.TryGetValue(apiKeyName, out var providedKey);
        return providedKey!.ToString();  // nem lehet null a filter miatt
    }
}
