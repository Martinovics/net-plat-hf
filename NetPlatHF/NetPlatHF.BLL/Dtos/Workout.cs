using NetPlatHF.DAL.Entities;

namespace NetPlatHF.BLL.Dtos;


public record Workout(
    string Name,
    Difficulty? Difficulty,
    IReadOnlyCollection<string> Groups
);
