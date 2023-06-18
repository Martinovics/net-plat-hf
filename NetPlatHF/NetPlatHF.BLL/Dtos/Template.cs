﻿namespace NetPlatHF.BLL.Dtos;




public record Template(
    int Id,
    int ExerciseId,
    string ExerciseName,
    string ExerciseMuscle,
    string ExerciseDescription,
    int Weight,
    int Repetitions,
    int GroupId,
    string GroupName,
    string GroupDescription
);
