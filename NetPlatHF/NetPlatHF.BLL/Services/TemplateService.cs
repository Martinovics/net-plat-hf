using Microsoft.EntityFrameworkCore;
using NetPlatHF.BLL.Dtos;
using NetPlatHF.BLL.Interfaces;
using NetPlatHF.DAL.Data;

namespace NetPlatHF.BLL.Services;




public class TemplateService : ITemplateService
{
    private readonly AppDbContext _ctx;


    public TemplateService(AppDbContext context)
    {
        _ctx = context;
    }




    public IReadOnlyCollection<Dtos.Template> List()
    {
        return _ctx.Templates
            .Include(x => x.Owner)
            .Include(x => x.Group)
            .Include(x => x.Exercise)
            .Where(x => x.Owner == null)
            .Select(x => ToModel(x.Group, x.Exercise, x.Weight, x.Repetitions))
            .ToList();
    }

    
    
    
    public IReadOnlyCollection<Dtos.Template> ListSelf(string userApiKey)
    {
        throw new NotImplementedException();
    }




    public Template GetById(int id, string? userApiKey)
    {
        throw new NotImplementedException();
    }



    public Template? Create(int groupId, int exerciseId)
    {
        throw new NotImplementedException();
    }




    public Template? Update()
    {
        throw new NotImplementedException();
    }




    public Template? Delete(int id)
    {
        throw new NotImplementedException();
    }
    

    

    private static Dtos.Template ToModel(DAL.Entities.GroupTemplate group, DAL.Entities.ExerciseTemplate exercise, int weight, int repetitions)
    {
        return new Dtos.Template(exercise.Id, exercise.Name, exercise.Muscle, exercise.Description, weight, repetitions, group.Id, group.Name, group.Description);
    }
}
