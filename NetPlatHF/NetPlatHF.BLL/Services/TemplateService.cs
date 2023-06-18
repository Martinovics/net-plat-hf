using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NetPlatHF.BLL.Dtos;
using NetPlatHF.BLL.Interfaces;
using NetPlatHF.DAL.Data;
using NetPlatHF.BLL.Exceptions;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace NetPlatHF.BLL.Services;




public class TemplateService : ITemplateService
{
    private readonly AppDbContext _ctx;
    private readonly IMapper _mapper;


    public TemplateService(AppDbContext context, IMapper mapper)
    {
        _ctx = context;
        _mapper = mapper;
    }




    public IReadOnlyCollection<Dtos.Template> List()
    {
        return _ctx.Templates
            .Include(x => x.Owner)
            .Include(x => x.Group)
            .Include(x => x.Exercise)
            .Where(x => x.Owner == null)
            .ProjectTo<Dtos.Template>(_mapper.ConfigurationProvider)
            .ToList();
    }

    
    
    
    public IReadOnlyCollection<Dtos.Template> ListSelf(string userApiKey)
    {
        return _ctx.Templates
            .Include(x => x.Owner)
            .Include(x => x.Group)
            .Include(x => x.Exercise)
            .Where(x => x.Owner!.ApiKey == userApiKey)  // nem lehet null, csak filter utan hivjuk
            .ProjectTo<Dtos.Template>(_mapper.ConfigurationProvider)
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
        
        return _mapper.Map<Dtos.Template>(template);
    }




    public Template? Update()
    {
        throw new NotImplementedException();
    }




    public Template? Delete(int id, string userApiKey)
    {
        var owner = GetUser(userApiKey);
        if (owner == null)
            return null;

        var template = _ctx.Templates
            .Include(x => x.Owner)
            .Include(x => x.Group)
            .Include(x => x.Exercise)
            .Where(x => x.OwnerId == owner.Id && x.Id == id)
            .SingleOrDefault();

        if (template == null)
            return null;

        _ctx.Templates.Remove(template);
        _ctx.SaveChanges();

        return _mapper.Map<Dtos.Template>(template);
    }
    



    private AppUser? GetUser(string userApiKey)  // TODO extract | dupe
    {
        return _ctx.Users.Where(u => u.ApiKey == userApiKey).SingleOrDefault();
    }

}
