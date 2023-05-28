using Microsoft.AspNetCore.Mvc;
using NetPlatHF.API.Authentication;
using NetPlatHF.BLL.Dtos;
using NetPlatHF.BLL.Interfaces;

namespace NetPlatHF.API.Controllers.v2;




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


    [MapToApiVersion("2.0")]
    [HttpGet]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public IEnumerable<BLL.Dtos.ExerciseTemplate> List()
    {
        throw new NotImplementedException();
    }
}
