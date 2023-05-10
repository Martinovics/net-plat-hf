using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


    public ExerciseTemplatesController(IExerciseTemplateService exerciseTemplateService)
    {
        _exerciseTemplateService = exerciseTemplateService;
    }




    [MapToApiVersion("1.0")]
    [HttpGet]
    public IEnumerable<ExerciseTemplate> Get([FromQuery] ExerciseTemplateQueryParamResolver resolvedParams)
    {
        return _exerciseTemplateService.GetExerciseTemplates(resolvedParams);
    }
}
