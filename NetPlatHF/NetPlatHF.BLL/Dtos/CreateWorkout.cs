using System.ComponentModel.DataAnnotations;

namespace NetPlatHF.BLL.Dtos;

public record CreateWorkout([Required] string Name);
