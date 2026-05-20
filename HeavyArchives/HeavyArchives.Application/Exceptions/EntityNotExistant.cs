namespace Exceptions
{
    [Serializable]
    public class EntityNotExistant : Exception
    {
        public EntityNotExistant():base("No entity found")
        {
        }

        public EntityNotExistant(string? message) : base(message)
        {
        }
    }
}