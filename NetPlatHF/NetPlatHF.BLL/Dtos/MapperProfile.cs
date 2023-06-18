using AutoMapper;


namespace NetPlatHF.BLL.Dtos;




public class MapperProfile: Profile
{
    public MapperProfile()
    {
        CreateMap<DAL.Entities.ExerciseTemplate, ExerciseTemplate>().ReverseMap();

        CreateMap<DAL.Entities.ExerciseTemplate, string>().ConvertUsing(src => src.Name);
        CreateMap<DAL.Entities.GroupTemplate, GroupTemplate>()
            /*.ForMember(
                dest => dest.Exercises,
                opt => opt.MapFrom(src => src.Exercises.Select(e => e.Name).ToList())
            )*/.ReverseMap();

        CreateMap<DAL.Entities.GroupExerciseTemplate, Template>().ReverseMap();

        CreateMap<DAL.Entities.GroupTemplate, string>().ConvertUsing(src => src.Name);
        CreateMap<DAL.Entities.Workout, Workout>().ReverseMap();
    }
}
