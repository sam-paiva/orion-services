namespace Orion.Core.Base
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public DateTime CreationDate { get; protected set; }
        public DateTime? LastModification { get; protected set; }
    }
}
