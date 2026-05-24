
namespace Exceptions
{
    [Serializable]
    internal class InvalidInformationException : Exception
    {
        public InvalidInformationException()
        {
        }

        public InvalidInformationException(string? message) : base(message)
        {
        }

        public InvalidInformationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}