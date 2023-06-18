namespace NetPlatHF.BLL.Dtos;


public record Workout(
    string Name,
    IReadOnlyCollection<string> Groups
);
