namespace Exceptions
{
    [Serializable]
    public class InvalidRefreshTokenException : Exception
    {
        public InvalidRefreshTokenException():base("Refresh token is not valid or has expired"){}

       
    }
}