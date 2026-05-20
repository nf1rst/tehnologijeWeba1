namespace Exceptions
{
    [Serializable]
    public class AuthenticationInvalidException : Exception
    {       
        public AuthenticationInvalidException(string message) : base(message)
        {
        }
    }
}