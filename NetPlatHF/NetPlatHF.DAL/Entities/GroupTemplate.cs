namespace NetPlatHF.DAL.Entities;




// Csoport templéteket, melyekből később egy edzéstervet állíthatunk össze
public class GroupTemplate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public ICollection<ExerciseTemplate> Exercises { get; } = new List<ExerciseTemplate>();
    public ICollection<GroupExerciseTemplate> GroupExercises { get; } = new List<GroupExerciseTemplate>();
    public int OwnerId { get; set; }


    public GroupTemplate(string name)
    {
        Name = name;
    }
}
