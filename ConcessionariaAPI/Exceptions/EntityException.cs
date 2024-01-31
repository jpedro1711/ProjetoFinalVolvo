namespace ConcessionariaAPI.Exceptions
{
    public class EntityException : Exception
    {
        public int StatusCode { get; }
        public EntityException() : base() { }

        public EntityException(string message) : base(message)
        {

        }

        public EntityException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
