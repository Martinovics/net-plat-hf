using System.ComponentModel.DataAnnotations;

namespace NetPlatHF.BLL.Dtos;

public record CreateGroupTemplate([Required] string Name, string? Description);
