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


    public ExerciseTemplateService(AppDbContext context)
    {
        _ctx = context;
    }




    public IReadOnlyCollection<Dtos.ExerciseTemplate> ListTemplates()
    {
        return _ctx.ExerciseTemplates.Include(x => x.Owner).Where(x => x.OwnerId == null).Select(ToModel).ToList();
    }




    public IReadOnlyCollection<Dtos.ExerciseTemplate> ListUserTemplates(string userApiKey)
    {
        return _ctx.ExerciseTemplates.Include(x => x.Owner).Where(x => x.Owner!.ApiKey == userApiKey).Select(ToModel).ToList();
    }




    public Dtos.ExerciseTemplate Insert(Dtos.CreateExerciseTemplate newTemplate, string userApiKey)
    {
        var transaction = _ctx.Database.BeginTransaction(IsolationLevel.RepeatableRead);
        
        var owner = GetOwner(userApiKey);
        var template = new ExerciseTemplate(newTemplate.Name)
        {
            Muscle = newTemplate?.Muscle ?? "",
            Description = newTemplate?.Description ?? "",
            Owner = owner
        };


        _ctx.ExerciseTemplates.Add(template);
        _ctx.SaveChanges();
        transaction.Commit();
        
        return ToModel(template);
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

        return template == null ? null : ToModel(template);
    }




    public Dtos.ExerciseTemplate? Update(int id, Dtos.UpdateExerciseTemplate newTemplate, string userApiKey)
    {
        var template = GetUserTemplate(id, userApiKey);
        if (template == null)
            return null;

        template.Name = newTemplate?.Name ?? template.Name;
        template.Muscle = newTemplate?.Muscle ?? template.Muscle;
        template.Description = newTemplate?.Description ?? template.Description;

        _ctx.SaveChanges();
        return ToModel(template);
    }




    public Dtos.ExerciseTemplate Delete(int id, string userApiKey)
    {
        throw new NotImplementedException();
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



    private Dtos.ExerciseTemplate ToModel(ExerciseTemplate exerciseTemplate)
    {
        return new Dtos.ExerciseTemplate(exerciseTemplate.Id, exerciseTemplate.Name, exerciseTemplate.Muscle, exerciseTemplate.Description);
    }

    
}




internal static class Extensions
{
    public static IQueryable<ExerciseTemplate> ApplyQueryParams(this IQueryable<ExerciseTemplate> query, ExerciseTemplateQueryParamResolver resolvedParams)
    {
        if (!string.IsNullOrEmpty(resolvedParams.Name))
        {
            query = query.Where(x => x.Name == resolvedParams.Name);
        }
        else if (!string.IsNullOrEmpty(resolvedParams.NameContains))
        {
            query = query.Where(x => x.Name.Contains(resolvedParams.NameContains));
        }

        if (!string.IsNullOrEmpty(resolvedParams.Muscle))
        {
            query = query.Where(x => x.Muscle == resolvedParams.Muscle);
        }
        else if (!string.IsNullOrEmpty(resolvedParams.MuscleContains))
        {
            query = query.Where(x => x.Muscle.Contains(resolvedParams.MuscleContains));
        }

        return query;
    }
}
