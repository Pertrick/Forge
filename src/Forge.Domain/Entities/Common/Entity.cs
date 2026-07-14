namespace Forge.Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get;}
        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}