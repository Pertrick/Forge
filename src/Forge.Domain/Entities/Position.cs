using Forge.Domain.Common;

namespace Forge.Domain.Entities
{
    
    public sealed class Position : Entity
    {
        public string Name { get; private set; }
        public bool IsRetired { get; private set; }

        public Position(string name)
        {
            SetName(name);
            SetIsRetired(false);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }

            Name = name;
        }

        private void SetIsRetired(bool isRetired)
        {
            IsRetired = isRetired;
        }


        public void Retire()
        {
            SetIsRetired(true);
        }


        public void EnsureNotRetired()
        {
            if (IsRetired)
            {
                throw new InvalidOperationException("This position is retired and cannot be used.");
            }
        }

    }
}