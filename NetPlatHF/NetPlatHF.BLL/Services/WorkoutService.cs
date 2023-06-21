using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using NetPlatHF.BLL.Dtos;
using NetPlatHF.BLL.Exceptions;
using NetPlatHF.BLL.Interfaces;
using NetPlatHF.DAL.Data;
using System.Data;

namespace NetPlatHF.BLL.Services;




public class WorkoutService : IWorkoutService
{
    private readonly AppDbContext _ctx;
    private readonly IMapper _mapper;


    public WorkoutService(AppDbContext context, IMapper mapper)
    {
        _ctx = context;
        _mapper = mapper;
    }




    public IReadOnlyCollection<Workout> List()
    {
        return _ctx.Workouts
            .Include(x => x.Owner)
            .Where(x => x.Owner == null)
            .ProjectTo<Dtos.Workout>(_mapper.ConfigurationProvider)
            .ToList();
    }




    public IReadOnlyCollection<Workout> ListSelf(string userApiKey)
    {
        return _ctx.Workouts
            .Include(x => x.Owner)
            .Include(x => x.Groups)
            .Where(x => x.Owner!.ApiKey == userApiKey)
            .ProjectTo<Dtos.Workout>(_mapper.ConfigurationProvider)
            .ToList();
    }




    public Workout? GetById(int id, string? userApiKey)
    {
        DAL.Entities.Workout? workout;
        if (userApiKey.IsNullOrEmpty())
        {
            workout = _ctx.Workouts.Include(x => x.Owner).Include(x => x.Groups).Where(x => x.Owner == null && x.Id == id).SingleOrDefault();
        }
        else
        {
            workout = GetUserWorkout(id, userApiKey!);
        }

        return workout == null ? null : _mapper.Map<Dtos.Workout>(workout);
    }




    public Workout? GetByName(string name, string? userApiKey)
    {
        DAL.Entities.Workout? workout;
        if (userApiKey.IsNullOrEmpty())
        {
            workout = _ctx.Workouts.Include(x => x.Owner).Include(x => x.Groups).Where(x => x.Owner == null && x.Name == name).SingleOrDefault();
        }
        else
        {
            workout = GetUserWorkout(name, userApiKey!);
        }

        return workout == null ? null : _mapper.Map<Dtos.Workout>(workout);
    }




    public Workout Create(CreateWorkout createWorkout, string userApiKey)
    {
        var owner = GetUser(userApiKey) ?? throw new InvalidOwnerException("Invalid owner");
        
        if (_ctx.Workouts.Any(x => x.Name == createWorkout.Name && x.OwnerId == owner.Id))
            throw new DuplicateWorkoutException($"A workout with name {createWorkout.Name} already exists");

        var workout = new DAL.Entities.Workout(createWorkout.Name)
        {
            Name = createWorkout.Name,
            Difficulty = createWorkout.Difficulty,
            Owner = owner
        };


        _ctx.Workouts.Add(workout);
        _ctx.SaveChanges();

        _ctx.Workouts.Include(x => x.Groups);
        return _mapper.Map<Dtos.Workout>(workout);
    }




    public Workout? Update()
    {
        throw new NotImplementedException();
    }




    public Workout? Add(string name, int groupId, string userApiKey)
    {
        var owner = GetUser(userApiKey);
        if (owner == null)
            return null;

        var workout = _ctx.Workouts.Include(x => x.Owner).Include(x => x.Groups).Where(x => x.Name == name && x.OwnerId == owner.Id).SingleOrDefault();
        if (workout == null)
            return null;

        var group = _ctx.GroupTemplates.Include(x => x.Owner).Where(x => x.Id == groupId && x.OwnerId == owner.Id).SingleOrDefault();
        if (group == null)
            return null;

        workout.Groups.Add(group);
        _ctx.SaveChanges();

        return _mapper.Map<Dtos.Workout>(workout);
    }




    public Workout? Remove(string name, int groupId, string userApiKey)
    {
        var owner = GetUser(userApiKey);
        if (owner == null)
            return null;

        var workout = _ctx.Workouts.Include(x => x.Owner).Include(x => x.Groups).Where(x => x.Name == name && x.OwnerId == owner.Id).SingleOrDefault();
        if (workout == null)
            return null;

        var group = _ctx.GroupTemplates.Include(x => x.Owner).Where(x => x.Id == groupId && x.OwnerId == owner.Id).SingleOrDefault();
        if (group == null)
            return null;

        workout.Groups.Remove(group);
        _ctx.SaveChanges();

        return _mapper.Map<Dtos.Workout>(workout);
    }




    public Workout? Delete(string name, string userApiKey)
    {
        var owner = GetUser(userApiKey);
        if (owner == null)
            return null;

        var workout = _ctx.Workouts.Include(x => x.Owner).Include(x => x.Groups).Where(x => x.Name == name && x.OwnerId == owner.Id).SingleOrDefault();
        if (workout == null)
            return null;

        _ctx.Workouts.Remove(workout);
        _ctx.SaveChanges();

        return _mapper.Map<Dtos.Workout>(workout);
    }




    private AppUser? GetUser(string userApiKey)  // TODO extract | dupe
    {
        return _ctx.Users.Where(u => u.ApiKey == userApiKey).SingleOrDefault();
    }




    private DAL.Entities.Workout? GetUserWorkout(string name, string apiKey)
    {
        var owner = GetUser(apiKey);
        if (owner == null)
            return null;

        return _ctx.Workouts.Include(x => x.Owner).Include(x => x.Groups).Where(x => x.Name == name && x.OwnerId == owner.Id).SingleOrDefault();
    }


    private DAL.Entities.Workout? GetUserWorkout(int id, string apiKey)
    {
        var owner = GetUser(apiKey);
        if (owner == null)
            return null;

        return _ctx.Workouts.Include(x => x.Owner).Include(x => x.Groups).Where(x => x.Id == id && x.OwnerId == owner.Id).SingleOrDefault();
    }


}
