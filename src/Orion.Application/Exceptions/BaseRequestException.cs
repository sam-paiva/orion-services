namespace Orion.Application.Exceptions
{
    public abstract class BaseRequestException : Exception
    {
        public abstract IReadOnlyDictionary<string, object> Errors { get; }

        public BaseRequestException(string message) : base(message)
        {

        }
    }
}
