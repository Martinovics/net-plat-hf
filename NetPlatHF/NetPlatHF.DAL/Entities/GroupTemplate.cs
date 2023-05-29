using NetPlatHF.DAL.Data;

namespace NetPlatHF.DAL.Entities;




// Csoport templéteket, melyekből később egy edzéstervet állíthatunk össze
public class GroupTemplate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = "";
    public ICollection<ExerciseTemplate> Exercises { get; } = new List<ExerciseTemplate>();
    public ICollection<GroupExerciseTemplate> GroupExercises { get; } = new List<GroupExerciseTemplate>();
    public string? OwnerId { get; set; }
    public AppUser? Owner { get; set; }


    public GroupTemplate(string name)
    {
        Name = name;
    }
}
