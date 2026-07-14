using Forge.Domain.Common;

namespace Forge.Domain.Entities
{
    public sealed class Department : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Department(string name, string description)
        {
            SetName(name);
            SetDescription(description);
        }
        

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }

            Name = name;
        } 

        private void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Description cannot be null or empty.", nameof(description));
            }

            Description = description;
        }
        

    }
}
