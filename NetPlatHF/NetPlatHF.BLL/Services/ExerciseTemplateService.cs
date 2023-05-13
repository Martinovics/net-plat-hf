using Microsoft.EntityFrameworkCore;
using NetPlatHF.BLL.Exceptions;
using NetPlatHF.BLL.Interfaces;
using NetPlatHF.BLL.QueryParamResolvers;
using NetPlatHF.DAL.Data;
using NetPlatHF.DAL.Entities;
using System;

namespace NetPlatHF.BLL.Services;




public class ExerciseTemplateService : IExerciseTemplateService
{
    private readonly AppDbContext _context;


    public ExerciseTemplateService(AppDbContext context)
    {
        _context = context;
    }

    

    public IEnumerable<ExerciseTemplate> GetExerciseTemplates(ExerciseTemplateQueryParamResolver resolvedParams)
    {
        var exerciseTemplates = _context.ExerciseTemplates.AsQueryable().ApplyQueryParams(resolvedParams).Where(x => x.OwnerId == null);
        return exerciseTemplates.ToList();
    }


    public IEnumerable<ExerciseTemplate> GetUserExerciseTemplates(ExerciseTemplateQueryParamResolver resolvedParams, string apiKey)
    {
        var exerciseTemplates = _context.ExerciseTemplates
            .Include(x => x.Owner)
            .AsQueryable()
            .ApplyQueryParams(resolvedParams)
            .Where(x => x.Owner.ApiKey == apiKey);
        return exerciseTemplates.ToList();
    }


    public ExerciseTemplate GetUserExerciseTemplate(int id, string apiKey)
    {
        var exerciseTemplate = _context.ExerciseTemplates.Include(x => x.Owner).Where(x => x.Id == id && x.Owner!.ApiKey == apiKey).SingleOrDefault();
        if (exerciseTemplate == null)
        {
            throw new ExerciseTemplateNotFoundException($"User exercise template with {id} not found");
        }
        return exerciseTemplate;
    }


    public ExerciseTemplate InsertUserExerciseTemplate(ExerciseTemplate newExerciseTemplate, string userApiKey)
    {
        var owner = _context.Users.Single(x => x.ApiKey == userApiKey);
        newExerciseTemplate.Owner = owner;
        _context.ExerciseTemplates.Add(newExerciseTemplate);
        _context.SaveChanges();
        return GetUserExerciseTemplate(newExerciseTemplate.Id, userApiKey);
    }

    
    public bool UpdateUserExerciseTemplate(int exerciseTemplateId, ExerciseTemplate updatedExerciseTemplate, string userApiKey)
    {
        
        return true;
    }


    public bool DeleteUserExerciseTemplate(int exerciseTemplateId, string userApiKey)
    {
        ExerciseTemplate exerciseTemplate;
        try
        {
            exerciseTemplate = GetUserExerciseTemplate(exerciseTemplateId, userApiKey);
        }
        catch (ExerciseTemplateNotFoundException)
        {
            return false;
        }
        _context.ExerciseTemplates.Remove(exerciseTemplate);
        _context.SaveChanges();
        return true;
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
