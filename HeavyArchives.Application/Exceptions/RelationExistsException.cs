namespace Exceptions
{
    [Serializable]
    public class RelationExistsException : Exception
    {
        public RelationExistsException():base("Relationsship between object is already existant")
        {
        }

        public RelationExistsException(string? message):base(message)
        {
        }
    }
}