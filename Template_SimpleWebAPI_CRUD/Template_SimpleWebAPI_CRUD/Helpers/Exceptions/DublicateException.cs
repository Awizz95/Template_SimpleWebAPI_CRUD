namespace Template_SimpleWebAPI_CRUD.Helpers.Exceptions
{
    public class DublicateException : Exception
    {
        public DublicateException()
        {
        }

        public DublicateException(string? message) : base(message)
        {
        }

        public DublicateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}