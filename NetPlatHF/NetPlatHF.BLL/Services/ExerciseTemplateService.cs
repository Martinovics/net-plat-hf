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
        var exerciseTemplates = _context.ExerciseTemplates.AsQueryable().ApplyQueryParams(resolvedParams);
        return exerciseTemplates.ToList();
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
