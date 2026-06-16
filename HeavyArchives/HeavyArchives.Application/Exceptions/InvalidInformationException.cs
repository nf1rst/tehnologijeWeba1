
namespace Exceptions
{
    [Serializable]
    internal class InvalidInformationException : Exception
    {
        public InvalidInformationException():base("Givnen information was not valid")
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