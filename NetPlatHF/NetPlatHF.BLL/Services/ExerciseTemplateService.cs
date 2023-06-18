using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetPlatHF.BLL.Interfaces;
using NetPlatHF.BLL.QueryParamResolvers;
using NetPlatHF.DAL.Data;
using NetPlatHF.DAL.Entities;
using System.Data;
using System.Diagnostics;

namespace NetPlatHF.BLL.Services;




public class ExerciseTemplateService : IExerciseTemplateService
{
    private readonly AppDbContext _ctx;
    private readonly IMapper _mapper;


    public ExerciseTemplateService(AppDbContext context, IMapper mapper)
    {
        _ctx = context;
        _mapper = mapper;
    }




    public IReadOnlyCollection<Dtos.ExerciseTemplate> ListTemplates()
    {
        return _ctx.ExerciseTemplates
            .Include(x => x.Owner)
            .Where(x => x.OwnerId == null)
            .ProjectTo<Dtos.ExerciseTemplate>(_mapper.ConfigurationProvider)
            .ToList();
    }




    public IReadOnlyCollection<Dtos.ExerciseTemplate> ListUserTemplates(string userApiKey)
    {
        return _ctx.ExerciseTemplates
            .Include(x => x.Owner)
            .Where(x => x.Owner!.ApiKey == userApiKey)
            .ProjectTo<Dtos.ExerciseTemplate>(_mapper.ConfigurationProvider)
            .ToList();
    }




    public Dtos.ExerciseTemplate Insert(Dtos.CreateExerciseTemplate newTemplate, string userApiKey)
    {
        var owner = GetOwner(userApiKey);
        var template = new ExerciseTemplate(newTemplate.Name)
        {
            Muscle = newTemplate?.Muscle ?? "",
            Description = newTemplate?.Description ?? "",
            Owner = owner
        };


        _ctx.ExerciseTemplates.Add(template);
        _ctx.SaveChanges();

        return _mapper.Map<Dtos.ExerciseTemplate>(template);
    }




    public Dtos.ExerciseTemplate? GetTemplateById(int id, string? apiKey)
    {
        ExerciseTemplate? template;
        if (apiKey.IsNullOrEmpty())
        {
            template = _ctx.ExerciseTemplates.Include(x => x.Owner).Where(x => x.Id == id && x.Owner == null).SingleOrDefault();
        }
        else
        {
            template = GetUserTemplate(id, apiKey);
        }

        return template == null ? null : _mapper.Map<Dtos.ExerciseTemplate>(template);
    }




    public Dtos.ExerciseTemplate? Update(int id, Dtos.UpdateExerciseTemplate newTemplate, string userApiKey)
    {
        var template = GetUserTemplate(id, userApiKey);
        if (template == null)
            return null;

        template.Name = newTemplate?.Name ?? template.Name;
        template.Muscle = newTemplate?.Muscle ?? template.Muscle;
        template.Description = newTemplate?.Description ?? template.Description;

        _ctx.SaveChanges();  // TODO ha nem sikerul frissiteni pl a name hossza miatt akkor dobhat hibat
        return _mapper.Map<Dtos.ExerciseTemplate>(template);
    }




    public Dtos.ExerciseTemplate? Delete(int id, string userApiKey)
    {
        var template = GetUserTemplate(id, userApiKey);
        if (template == null)
            return null;

        _ctx.ExerciseTemplates.Remove(template);
        _ctx.SaveChanges();

        return _mapper.Map<Dtos.ExerciseTemplate>(template);
    }




    private AppUser? GetOwner(string apiKey)  // ne legyen nullozhato
    {
        return _ctx.Users.Where(u => u.ApiKey == apiKey).SingleOrDefault();
    }




    private ExerciseTemplate? GetUserTemplate(int id, string apiKey)
    {
        var owner = GetOwner(apiKey);
        if (owner == null)
            return null;

        var ownerID = owner.Id;
        return _ctx.ExerciseTemplates.Where(x => x.Id == id && x.OwnerId == ownerID).SingleOrDefault();
    }
}
