using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NetPlatHF.BLL.Dtos;
using NetPlatHF.BLL.Interfaces;
using NetPlatHF.DAL.Data;
using NetPlatHF.BLL.Exceptions;

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
            .Select(x => ToModel(x.Id, x.Group, x.Exercise, x.Weight, x.Repetitions))
            .ToList();
    }

    
    
    
    public IReadOnlyCollection<Dtos.Template> ListSelf(string userApiKey)
    {
        return _ctx.Templates
            .Include(x => x.Owner)
            .Include(x => x.Group)
            .Include(x => x.Exercise)
            .Where(x => x.Owner!.ApiKey == userApiKey)  // nem lehet null, csak filter utan hivjuk
            .Select(x => ToModel(x.Id, x.Group, x.Exercise, x.Weight, x.Repetitions))
            .ToList();
    }




    public Template GetById(int id, string? userApiKey)
    {
        throw new NotImplementedException();
    }




    public Template Create(int groupId, int exerciseId, CreateTemplate createTemplate, string userApiKey)  // TODO specifikusabb hibak
    {
        var owner = GetUser(userApiKey) ?? throw new InvalidOwnerException("Could not find user. The owner is invalid.");
        
        var group = _ctx.GroupTemplates.Include(x => x.Owner).Where(x => x.OwnerId == owner.Id && x.Id == groupId).SingleOrDefault()
                    ?? throw new GroupTemplateNotFoundException($"Could not found group template with id {groupId}");
        var exercise = _ctx.ExerciseTemplates.Include(x => x.Owner).Where(x => x.OwnerId == owner.Id && x.Id == exerciseId).SingleOrDefault()
                       ?? throw new ExerciseTemplateNotFoundException($"Could not found exercise template with id {exerciseId}");

        
        var template = new DAL.Entities.GroupExerciseTemplate()
        {
            Weight = createTemplate.Weight,
            Repetitions = createTemplate.Repetitions,
            Group = group,
            Exercise = exercise,
            Owner = owner,
        };
        _ctx.Templates.Add(template);
        _ctx.SaveChanges();
        
        return ToModel(template.Id, group, exercise, createTemplate.Weight, createTemplate.Repetitions);
    }




    public Template? Update()
    {
        throw new NotImplementedException();
    }




    public Template? Delete(int id)
    {
        throw new NotImplementedException();
    }
    



    private AppUser? GetUser(string userApiKey)  // TODO extract | dupe
    {
        return _ctx.Users.Where(u => u.ApiKey == userApiKey).SingleOrDefault();
    }




    private static Dtos.Template ToModel(int id, DAL.Entities.GroupTemplate group, DAL.Entities.ExerciseTemplate exercise, int weight, int repetitions)
    {
        return new Dtos.Template(id, exercise.Id, exercise.Name, exercise.Muscle, exercise.Description, weight, repetitions, group.Id, group.Name, group.Description);
    }


}
