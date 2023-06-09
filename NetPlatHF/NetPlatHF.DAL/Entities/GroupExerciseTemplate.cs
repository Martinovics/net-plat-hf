﻿using NetPlatHF.DAL.Data;

namespace NetPlatHF.DAL.Entities;




// kapcsolótábla az ExerciseTemplate és a GroupTemplate között
public class GroupExerciseTemplate
{
    public int Id { get; set; }
    public int Weight { get; set; }
    public int Repetitions { get; set; }
    public int ExerciseId { get; set; }
    public ExerciseTemplate Exercise { get; set; } = null!;
    public int GroupId { get; set; }
    public GroupTemplate Group { get; set; } = null!;
    public string? OwnerId { get; set; }  // foreign key 
    public AppUser? Owner { get; set; }
}
