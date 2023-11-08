namespace CloudSuite.Modules.Domain.Exceptions
{
    public abstract class BadResquestException : ApplicationException
    {
        protected BadResquestException(string message)
            : base("Bad Request", message)
        {
        }

    }
}
