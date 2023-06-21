using NetPlatHF.DAL.Data;

namespace NetPlatHF.DAL.Entities;


[Flags]
public enum Difficulty
{
    Easy = 1,
    Medium = 2,
    Hard = 4,
}


public class Workout
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Difficulty? Difficulty { get; set; }
    public ICollection<GroupTemplate> Groups { get; } = new List<GroupTemplate>();
    public string? OwnerId { get; set; }
    public AppUser? Owner { get; set; }


    public Workout(string name)
    {
        Name = name;
    }
}
