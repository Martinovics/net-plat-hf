namespace NetPlatHF.BLL.QueryParamResolvers;




public class ExerciseTemplateQueryParamResolver
{
    public string? Name { get; set; }
    public string? NameContains { get; set; }
    public string? Muscle { get; set; }
    public string? MuscleContains { get; set; }
    public string? OrderBy { get; set; }  // oszlop nev
    public string? Sort { get; set; } = "asc";
}
