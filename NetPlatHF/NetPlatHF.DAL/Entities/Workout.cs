using NetPlatHF.DAL.Data;

namespace NetPlatHF.DAL.Entities;


public class Workout
{
    public string Name { get; set; }
    public ICollection<GroupTemplate> Groups { get; } = new List<GroupTemplate>();
    public string? OwnerId { get; set; }
    public AppUser? Owner { get; set; }


    public Workout(string name)
    {
        Name = name;
    }
}
