using Forge.Domain.Common;

namespace Forge.Domain.Entities
{
    public sealed class Employee : Entity
    {
       public string Name { get; private set; }
       public Guid DepartmentId { get; private set; }
       private readonly List<Compensation> _compensations = new();
       public IReadOnlyList<Compensation> Compensations => _compensations.AsReadOnly();
       public bool IsActive { get; private set; }

        public Employee(string name, Guid departmentId)
        {
            SetName(name);
            AssignDepartment(departmentId);
            Hire();
        }


        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }

            Name = name;
        }

        public void AssignDepartment(Guid departmentId)
        {
            if (departmentId == Guid.Empty)
            {
                throw new ArgumentException("Department ID cannot be empty.", nameof(departmentId));
            }

            this.departmentId = departmentId;
        }

        public void AddCompensation(Compensation compensation)
        {
            if (compensation is null)
            {
                throw new ArgumentException("Compensation cannot be null.", nameof(compensation));
            }

            _compensations.Add(compensation);
        }

        public void AddCompensations(IEnumerable<Compensation> compensations)
        {
            if(compensations is null)
            {
                throw new ArgumentException("Compensations cannot be null or empty.", nameof(compensations));
            }

            foreach (var compensation in compensations)
            {
                AddCompensation(compensation);
            }
        }

        private void SetIsActive(bool value)
        {
            if(IsActive == value)
            {
                throw new InvalidOperationException($"Employee is already {(value ? "active" : "inactive")}.");
            }

            IsActive = value;
        }   

        public void Hire()
        {
            SetIsActive(true);
        }

        public void TerminateEmployment()
        {
            SetIsActive(false);
        }
    }
}
