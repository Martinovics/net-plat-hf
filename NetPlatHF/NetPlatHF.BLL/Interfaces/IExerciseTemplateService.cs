using NetPlatHF.BLL.QueryParamResolvers;
using NetPlatHF.DAL.Entities;

namespace NetPlatHF.BLL.Interfaces;




public interface IExerciseTemplateService
{
    IEnumerable<ExerciseTemplate> GetExerciseTemplates(ExerciseTemplateQueryParamResolver resolvedParams);
}
