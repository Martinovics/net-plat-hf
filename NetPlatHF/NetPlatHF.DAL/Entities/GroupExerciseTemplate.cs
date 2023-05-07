namespace NetPlatHF.DAL.Entities;




// kapcsolótábla az ExerciseTemplate és a GroupTemplate között
public class GroupExerciseTemplate
{
    public int Id { get; set; }
    public int ExerciseId { get; set; }
    public ExerciseTemplate Exercise { get; set; } = null!;
    public int GroupId { get; set; }
    public GroupTemplate Group { get; set; } = null!;
}
