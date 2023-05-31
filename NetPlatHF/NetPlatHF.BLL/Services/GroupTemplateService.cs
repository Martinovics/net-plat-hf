using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetPlatHF.BLL.Interfaces;
using NetPlatHF.BLL.QueryParamResolvers;
using NetPlatHF.DAL.Data;
using NetPlatHF.DAL.Entities;
using System.Data;
using System.Diagnostics;

namespace NetPlatHF.BLL.Services;




public class GroupTemplateService : IGroupTemplateService
{
    private readonly AppDbContext _ctx;


    public GroupTemplateService(AppDbContext context)
    {
        _ctx = context;
    }




    public IReadOnlyCollection<Dtos.GroupTemplate> ListTemplates()
    {
        return _ctx.GroupTemplates.Include(x => x.Owner).Where(x => x.OwnerId == null).Select(ToModel).ToList();
    }




    public IReadOnlyCollection<Dtos.GroupTemplate> ListUserTemplates(string userApiKey)
    {
        return _ctx.GroupTemplates.Include(x => x.Owner).Where(x => x.Owner!.ApiKey == userApiKey).Select(ToModel).ToList();
    }




    public Dtos.GroupTemplate Insert(Dtos.CreateGroupTemplate newTemplate, string userApiKey)
    {
        var transaction = _ctx.Database.BeginTransaction(IsolationLevel.RepeatableRead);

        var owner = GetOwner(userApiKey);
        var template = new GroupTemplate(newTemplate.Name)
        {
            Description = newTemplate?.Description ?? "",
            Owner = owner
        };


        _ctx.GroupTemplates.Add(template);
        _ctx.SaveChanges();
        transaction.Commit();

        return ToModel(template);
    }




    public Dtos.GroupTemplate? GetTemplateById(int id, string? apiKey)
    {
        GroupTemplate? template;
        if (apiKey.IsNullOrEmpty())
        {
            template = _ctx.GroupTemplates.Include(x => x.Owner).Include(x => x.Exercises).Where(x => x.Id == id && x.Owner == null).SingleOrDefault();
        }
        else
        {
            template = GetUserTemplate(id, apiKey!);
        }

        return template == null ? null : ToModel(template);
    }




    public Dtos.GroupTemplate? Update(int id, Dtos.UpdateGroupTemplate newTemplate, string userApiKey)
    {
        var template = GetUserTemplate(id, userApiKey);
        if (template == null)
            return null;

        template.Name = newTemplate?.Name ?? template.Name;
        template.Description = newTemplate?.Description ?? template.Description;

        _ctx.SaveChanges();  // TODO ha nem sikerul frissiteni pl a name hossza miatt akkor dobhat hibat
        return ToModel(template);
    }




    public Dtos.GroupTemplate? Delete(int id, string userApiKey)
    {
        var template = GetUserTemplate(id, userApiKey);
        if (template == null)
            return null;

        _ctx.GroupTemplates.Remove(template);
        _ctx.SaveChanges();

        return ToModel(template);
    }




    private AppUser? GetOwner(string apiKey)  // ne legyen nullozhato
    {
        return _ctx.Users.Where(u => u.ApiKey == apiKey).SingleOrDefault();
    }



    private GroupTemplate? GetUserTemplate(int id, string apiKey)
    {
        var owner = GetOwner(apiKey);
        if (owner == null)
            return null;

        var ownerID = owner.Id;
        return _ctx.GroupTemplates.Include(x => x.Exercises).Where(x => x.Id == id && x.OwnerId == ownerID).SingleOrDefault();
    }




    private Dtos.GroupTemplate ToModel(GroupTemplate groupTemplate)
    {
        var exercises = new List<string>();
        foreach (var exercise in groupTemplate.Exercises)
            exercises.Add(exercise.Name);

        return new Dtos.GroupTemplate(groupTemplate.Id, groupTemplate.Name, groupTemplate.Description, 0 < exercises.Count ? exercises : null);
    }


}
