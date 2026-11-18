namespace Exceptions
{
    [Serializable]
    public class InvalidInputException : Exception
    {
        public InvalidInputException():base("Input is not valid")
        {
        }

        public InvalidInputException(string? message) : base(message)
        {

        }

    }
}