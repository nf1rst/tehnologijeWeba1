namespace Exceptions
{
    [Serializable]
    internal class RelationExistsException : Exception
    {
        public RelationExistsException():base("Relationsship between object is already existant")
        {
        }

        public RelationExistsException(string? message)
        {
        }
    }
}