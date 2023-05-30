namespace NetPlatHF.BLL.Exceptions;




public class InvalidOwnerException : Exception
{
    public InvalidOwnerException()
    {
    }

    public InvalidOwnerException(string message) : base(message)
    {
    }

    public InvalidOwnerException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
