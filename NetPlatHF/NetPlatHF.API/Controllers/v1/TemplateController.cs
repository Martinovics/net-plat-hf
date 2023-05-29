using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


}
