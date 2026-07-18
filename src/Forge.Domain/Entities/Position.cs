namespace Forge.Domain.Entities
{
    using Forge.Domain.Common;
    
    public sealed class Position : Entity
    {
        public string Name { get; private set; }

        public Position(string name)
        {
            SetName(name);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }

            Name = name;
        }

    }
}