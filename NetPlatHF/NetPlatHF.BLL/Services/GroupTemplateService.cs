using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetPlatHF.BLL.Dtos;
using NetPlatHF.BLL.Interfaces;
using NetPlatHF.DAL.Data;
using NetPlatHF.DAL.Entities;
using System.Data;

namespace NetPlatHF.BLL.Services;




public class GroupTemplateService : IGroupTemplateService
{
    private readonly AppDbContext _ctx;
    private readonly IMapper _mapper;


    public GroupTemplateService(AppDbContext context, IMapper mapper)
    {
        _ctx = context;
        _mapper = mapper;
    }




    public IReadOnlyCollection<Dtos.GroupTemplate> ListTemplates()
    {
        return _ctx.GroupTemplates
            .Include(x => x.Owner)
            .Where(x => x.OwnerId == null)
            .ProjectTo<Dtos.GroupTemplate>(_mapper.ConfigurationProvider)
            .ToList();
    }




    public IReadOnlyCollection<Dtos.GroupTemplate> ListUserTemplates(string userApiKey)
    {
        return _ctx.GroupTemplates
            .Include(x => x.Owner)
            .Where(x => x.Owner!.ApiKey == userApiKey)
            .ProjectTo<Dtos.GroupTemplate>(_mapper.ConfigurationProvider)
            .ToList();
    }




    public Dtos.GroupTemplate Insert(Dtos.CreateGroupTemplate newTemplate, string userApiKey)
    {
        var owner = GetOwner(userApiKey);
        var template = new DAL.Entities.GroupTemplate(newTemplate.Name)
        {
            Description = newTemplate?.Description ?? "",
            Owner = owner
        };


        _ctx.GroupTemplates.Add(template);
        _ctx.SaveChanges();

        return _mapper.Map<Dtos.GroupTemplate>(template);
    }




    public Dtos.GroupTemplate InsertGroupBulkedExercises(CreateGroupBulkedExercise newGroup, string userApiKey)
    {

        var owner = GetOwner(userApiKey);
        var group = new DAL.Entities.GroupTemplate(newGroup.Group.Name)
        {
            Description = newGroup.Group.Description ?? "",
            Owner = owner
        };

        foreach (var newExercise in newGroup.Exercises)
        {
            var exercise = new DAL.Entities.ExerciseTemplate(newExercise.Name)
            {
                Muscle = newExercise?.Muscle ?? "",
                Description = newExercise?.Description ?? "",
                Owner = owner
            };
            group.Exercises.Add(exercise);
        }


        _ctx.GroupTemplates.Add(group);
        _ctx.SaveChanges();

        return _mapper.Map<Dtos.GroupTemplate>(group);
    }




    public Dtos.GroupTemplate? GetTemplateById(int id, string? apiKey)
    {
        DAL.Entities.GroupTemplate? template;
        if (apiKey.IsNullOrEmpty())
        {
            template = _ctx.GroupTemplates.Include(x => x.Owner).Include(x => x.Exercises).Where(x => x.Id == id && x.Owner == null).SingleOrDefault();
        }
        else
        {
            template = GetUserTemplate(id, apiKey!);
        }

        return template == null ? null : _mapper.Map<Dtos.GroupTemplate>(template);
    }




    public Dtos.GroupTemplate? Update(int id, Dtos.UpdateGroupTemplate newTemplate, string userApiKey)
    {
        var template = GetUserTemplate(id, userApiKey);
        if (template == null)
            return null;

        template.Name = newTemplate?.Name ?? template.Name;
        template.Description = newTemplate?.Description ?? template.Description;

        _ctx.SaveChanges();  // TODO ha nem sikerul frissiteni pl a name hossza miatt akkor dobhat hibat
        return _mapper.Map<Dtos.GroupTemplate>(template);
    }




    public Dtos.GroupTemplate? Delete(int id, string userApiKey)
    {
        var template = GetUserTemplate(id, userApiKey);
        if (template == null)
            return null;

        _ctx.GroupTemplates.Remove(template);
        _ctx.SaveChanges();

        return _mapper.Map<Dtos.GroupTemplate>(template);
    }




    private AppUser? GetOwner(string apiKey)  // ne legyen nullozhato
    {
        return _ctx.Users.Where(u => u.ApiKey == apiKey).SingleOrDefault();
    }




    private DAL.Entities.GroupTemplate? GetUserTemplate(int id, string apiKey)
    {
        var owner = GetOwner(apiKey);
        if (owner == null)
            return null;

        var ownerID = owner.Id;
        return _ctx.GroupTemplates.Include(x => x.Exercises).Where(x => x.Id == id && x.OwnerId == ownerID).SingleOrDefault();
    }

}
