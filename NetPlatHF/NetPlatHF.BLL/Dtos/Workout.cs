using NetPlatHF.DAL.Entities;

namespace NetPlatHF.BLL.Dtos;


public record Workout(
    int Id,
    string Name,
    Difficulty? Difficulty,
    IReadOnlyCollection<string> Groups
);
