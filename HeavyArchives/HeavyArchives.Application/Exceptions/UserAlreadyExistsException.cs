namespace Exceptions
{
    [Serializable]
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string username):base($"Username '{username}' already exists"){}     
    }
}