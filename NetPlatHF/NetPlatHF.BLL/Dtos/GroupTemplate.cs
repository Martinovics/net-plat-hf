namespace NetPlatHF.BLL.Dtos;

public record GroupTemplate(int Id, string Name, string Description, IReadOnlyCollection<string>? Exercises = null);
