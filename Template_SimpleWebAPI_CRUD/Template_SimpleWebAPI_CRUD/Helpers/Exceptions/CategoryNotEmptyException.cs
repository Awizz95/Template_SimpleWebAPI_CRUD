namespace Template_SimpleWebAPI_CRUD.Helpers.Exceptions
{
    [Serializable]
    internal class CategoryNotEmptyException : Exception
    {
        public CategoryNotEmptyException()
        {
        }

        public CategoryNotEmptyException(string? message) : base(message)
        {
        }

        public CategoryNotEmptyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}