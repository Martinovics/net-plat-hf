namespace NetPlatHF.BLL.Exceptions;




public class DuplicateWorkoutException : Exception
{
    public DuplicateWorkoutException()
    {
    }

    public DuplicateWorkoutException(string message) : base(message)
    {
    }

    public DuplicateWorkoutException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
