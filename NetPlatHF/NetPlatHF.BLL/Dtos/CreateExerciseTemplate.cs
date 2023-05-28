using System.ComponentModel.DataAnnotations;

namespace NetPlatHF.BLL.Dtos;

public record CreateExerciseTemplate([Required] string Name, string? Muscle, string? Description);
