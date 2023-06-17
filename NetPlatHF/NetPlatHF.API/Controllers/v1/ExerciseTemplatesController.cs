﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetPlatHF.API.Authentication;
using NetPlatHF.BLL.Exceptions;
using NetPlatHF.BLL.Interfaces;
using NetPlatHF.BLL.QueryParamResolvers;
using NetPlatHF.BLL.Services;
using NetPlatHF.DAL.Entities;
using System.Net.Http.Headers;
using NetPlatHF.BLL.Dtos;
using Newtonsoft.Json.Linq;


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




    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<BLL.Dtos.ExerciseTemplate> List()
    {
        return _exerciseTemplateService.ListTemplates();
    }




    [HttpGet("self")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public IEnumerable<BLL.Dtos.ExerciseTemplate> ListSelf()
    {
        var apiKey = FetchApiKey(HttpContext);
        return _exerciseTemplateService.ListUserTemplates(apiKey);
    }




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
