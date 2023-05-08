using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace NetPlatHF.API.Controllers.v1;




[ApiController]
[Route("api/v{version:apiVersion}/exercise/templates")]
[ApiVersion("1.0")]

[ApiVersion("2.0")]
public class ExerciseTemplatesController : ControllerBase
{
    [MapToApiVersion("1.0")]
    [HttpGet]
    public string Get() => "Hello from api 1";
}
