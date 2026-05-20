namespace Exceptions
{
    [Serializable]
    public class NoRelationException : Exception
    {
        public NoRelationException():base("No relation found between entities")
        {
        }

        public NoRelationException(string text)
        {
        }
    }
}