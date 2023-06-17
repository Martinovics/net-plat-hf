using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPlatHF.BLL.Dtos;

public record CreateGroupBulkedExercise(
    CreateGroupTemplate Group,
    List<CreateExerciseTemplate> Exercises
);
