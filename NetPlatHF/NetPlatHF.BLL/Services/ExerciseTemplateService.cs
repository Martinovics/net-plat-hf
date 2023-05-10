using NetPlatHF.BLL.Interfaces;
using NetPlatHF.DAL.Data;
using System;

namespace NetPlatHF.BLL.Services;




public class ExerciseTemplateService : IExerciseTemplateService
{
    AppDbContext _context;

    public ExerciseTemplateService(AppDbContext context)
    {
        _context = context;
    }
}
