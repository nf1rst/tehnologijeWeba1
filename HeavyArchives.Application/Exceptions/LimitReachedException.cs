
namespace Exceptions
{
    [Serializable]
    public class LimitReachedException : Exception
    {
        public LimitReachedException():base("Limit has been reached, cannot proceed")
        {
        }

        public LimitReachedException(string? message) : base(message)
        {
        }
    }
}