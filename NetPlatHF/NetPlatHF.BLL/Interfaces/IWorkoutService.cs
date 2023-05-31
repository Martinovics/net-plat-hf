using NetPlatHF.BLL.Dtos;

namespace NetPlatHF.BLL.Interfaces;




public interface IWorkoutService
{
    public IReadOnlyCollection<Dtos.Workout> List();
    public IReadOnlyCollection<Dtos.Workout> ListSelf(string userApiKey);
    public Dtos.Workout? GetByName(string name, string? userApiKey);
    public Dtos.Workout Create(CreateWorkout createWorkout, string userApiKey);
    public Dtos.Workout? Add(string name, int groupId, string userApiKey);
    public Dtos.Workout? Remove(string name, int groupId, string userApiKey);
    public Dtos.Workout? Update();
    public Dtos.Workout? Delete(string name, string userApiKey);
}
