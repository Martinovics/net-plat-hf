using NetPlatHF.BLL.Dtos;
using NetPlatHF.BLL.QueryParamResolvers;
using NetPlatHF.DAL.Entities;

namespace NetPlatHF.BLL.Interfaces;




public interface IExerciseTemplateService
{
    public IReadOnlyCollection<Dtos.ExerciseTemplate> ListTemplates();
    public IReadOnlyCollection<Dtos.ExerciseTemplate> ListUserTemplates(string userApiKey);
    public Dtos.ExerciseTemplate Insert(CreateExerciseTemplate template, string userApiKey);
    public Dtos.ExerciseTemplate? Update(int id, UpdateExerciseTemplate template, string userApiKey);
    public Dtos.ExerciseTemplate? Delete(int id, string userApiKey);
    public Dtos.ExerciseTemplate? GetTemplateById(int id, string? userApiKey);
}
