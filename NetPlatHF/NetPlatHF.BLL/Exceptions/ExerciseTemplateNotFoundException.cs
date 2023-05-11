namespace NetPlatHF.BLL.Exceptions;




public class ExerciseTemplateNotFoundException : Exception
{
    public ExerciseTemplateNotFoundException()
    {
    }

    public ExerciseTemplateNotFoundException(string message) : base(message)
    {
    }

    public ExerciseTemplateNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
