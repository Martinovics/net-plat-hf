using NetPlatHF.DAL.Data;
using System.ComponentModel.DataAnnotations;

namespace NetPlatHF.DAL.Entities;




// Gyakorlat templétek, melyekből csoport templéteket állíthatunk össze
public class ExerciseTemplate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Muscle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<GroupTemplate> Groups { get; } = new List<GroupTemplate>();
    public ICollection<GroupExerciseTemplate> GroupExercises { get; } = new List<GroupExerciseTemplate>();
    public string? OwnerId { get; set; }  // foreign key 
    public AppUser? Owner { get; set; }


    public ExerciseTemplate(string name)
    {
        Name = name;
    }
}
