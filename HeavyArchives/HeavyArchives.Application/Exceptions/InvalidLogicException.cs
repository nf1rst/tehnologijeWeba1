namespace Exceptions
{
    [Serializable]
    public class InvalidLogicException : Exception
    {
        public InvalidLogicException():base("Invalid logic, operation cannot preoceed")
        {
        }

        public InvalidLogicException(string? message) : base(message)
        {
        }
    }
}