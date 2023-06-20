using NetPlatHF.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace NetPlatHF.BLL.Dtos;

public record CreateWorkout([Required] string Name, Difficulty? Difficulty);
