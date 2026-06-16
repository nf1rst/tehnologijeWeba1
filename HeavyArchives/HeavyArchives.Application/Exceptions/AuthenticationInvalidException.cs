namespace Exceptions
{
    [Serializable]
    public class AuthenticationInvalidException : Exception
    {
        public AuthenticationInvalidException():base("Authentication was not valid")
        {
            
        }
        public AuthenticationInvalidException(string message) : base(message)
        {
        }
    }
}