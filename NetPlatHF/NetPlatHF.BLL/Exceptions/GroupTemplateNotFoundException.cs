namespace NetPlatHF.BLL.Exceptions;




public class GroupTemplateNotFoundException : Exception
{
    public GroupTemplateNotFoundException()
    {
    }

    public GroupTemplateNotFoundException(string message) : base(message)
    {
    }

    public GroupTemplateNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
