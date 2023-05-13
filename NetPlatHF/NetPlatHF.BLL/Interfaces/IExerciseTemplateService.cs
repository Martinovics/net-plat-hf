﻿using NetPlatHF.BLL.QueryParamResolvers;
using NetPlatHF.DAL.Entities;

namespace NetPlatHF.BLL.Interfaces;




public interface IExerciseTemplateService
{
    public IEnumerable<ExerciseTemplate> GetExerciseTemplates(ExerciseTemplateQueryParamResolver resolvedParams);
    public IEnumerable<ExerciseTemplate> GetUserExerciseTemplates(ExerciseTemplateQueryParamResolver resolvedParams, string apiKey);
    public ExerciseTemplate GetUserExerciseTemplate(int id, string apiKey);
    public ExerciseTemplate InsertUserExerciseTemplate(ExerciseTemplate newExerciseTemplate, string userApiKey);
    public bool UpdateUserExerciseTemplate(int exerciseTemplateId, ExerciseTemplate updatedExerciseTemplate, string userApiKey);
    public bool DeleteUserExerciseTemplate(int exerciseTemplateId, string userApiKey);
}
