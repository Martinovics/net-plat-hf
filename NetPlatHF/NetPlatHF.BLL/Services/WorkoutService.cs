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


    public WorkoutService(AppDbContext context)
    {
        _ctx = context;
    }




    public IReadOnlyCollection<Workout> List()
    {
        return _ctx.Workouts.Include(x => x.Owner).Where(x => x.Owner == null).Select(ToModel).ToList();
    }




    public IReadOnlyCollection<Workout> ListSelf(string userApiKey)
    {
        return _ctx.Workouts.Include(x => x.Owner).Include(x => x.Groups).Where(x => x.Owner!.ApiKey == userApiKey).Select(ToModel).ToList();
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

        return workout == null ? null : ToModel(workout);
    }




    public Workout Create(CreateWorkout createWorkout, string userApiKey)
    {
        var transaction = _ctx.Database.BeginTransaction(IsolationLevel.RepeatableRead);

        var owner = GetUser(userApiKey) ?? throw new InvalidOwnerException("Invalid owner");
        
        if (_ctx.Workouts.Any(x => x.Name == createWorkout.Name && x.OwnerId == owner.Id))
            throw new DuplicateWorkoutException($"A workout with name {createWorkout.Name} already exists");

        var workout = new DAL.Entities.Workout(createWorkout.Name)
        {
            Name = createWorkout.Name,
            Owner = owner
        };


        _ctx.Workouts.Add(workout);
        _ctx.SaveChanges();
        transaction.Commit();

        _ctx.Workouts.Include(x => x.Groups);
        return ToModel(workout);
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

        return ToModel(workout);
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

        return ToModel(workout);
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

        return ToModel(workout);
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




    private static Dtos.Workout ToModel(DAL.Entities.Workout workout)
    {
        var groupNames = new List<string>();
        foreach (var group in workout.Groups)
            groupNames.Add(group.Name);

        return new Dtos.Workout(workout.Name, groupNames);
    }


}
