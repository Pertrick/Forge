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
       public Guid PositionId { get; private set; }

        public Employee(string name, Guid departmentId, Guid positionId)
        {
            SetName(name);
            AssignDepartment(departmentId);
            AssignInitialPosition(positionId);
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

        private void AssignDepartment(Guid departmentId)
        {
            if (departmentId == Guid.Empty)
            {
                throw new ArgumentException("Department ID cannot be empty.", nameof(departmentId));
            }

            DepartmentId = departmentId;
        }

        public void AddCompensation(CompensationType type, Money money, DateTime? effectiveDate = null, DateTime? endDate = null)
        {
            var compensation = new Compensation(type, money, effectiveDate, endDate);
            var activeCompensation = _compensations.FirstOrDefault(c => c.IsActive && c.Type == type);

            if (activeCompensation != null)
            {
                activeCompensation.End(DateTime.UtcNow);
            }

            _compensations.Add(compensation);
        }   

        // Child-entity mutation flows through the aggregate root: callers name the
        // compensation by identity and the root reaches it, rather than mutating a
        // Compensation reference directly.
        public void EndCompensation(Guid compensationId, DateTime endDate)
        {
            var compensation = _compensations.FirstOrDefault(c => c.Id == compensationId);
            if (compensation is null)
            {
                throw new InvalidOperationException("Compensation not found for this employee.");
            }

            compensation.End(endDate);
        }

        private void SetIsActive(bool value)
        {
            if(IsActive == value)
            {
                throw new InvalidOperationException($"Employee is already {(value ? "active" : "inactive")}.");
            }

            IsActive = value;
        }   

        private void Hire()
        {
            if(IsActive)
            {
              throw new InvalidOperationException("Employee has already been hired.");
            }

            SetIsActive(true);
        }

        public void Terminate()
        {
            SetIsActive(false);
        }

        private void SetPosition(Guid positionId)
        {
            PositionId = positionId;
        }

        private void AssignInitialPosition(Guid positionId)
        {
            if (positionId == Guid.Empty)
            {
                throw new ArgumentException("Position ID cannot be empty.", nameof(positionId));
            }

            SetPosition(positionId);
        }


        private void ChangePosition(Guid newPositionId)
        {
            if (newPositionId == Guid.Empty)
            {
                throw new ArgumentException("New position ID cannot be empty.", nameof(newPositionId));
            }

            if (newPositionId == PositionId)
            {
                throw new InvalidOperationException("New position ID cannot be the same as the current position ID.");
            }

            if(IsActive == false)
            {
                throw new InvalidOperationException("Cannot change position for a terminated employee.");
            }

            SetPosition(newPositionId);
        }

        public void Promote(Guid newPositionId)
        {
            ChangePosition(newPositionId);
        }

        public void TransferToDepartment(Guid newDepartmentId)
        {
            if(newDepartmentId == DepartmentId)
            {
                throw new InvalidOperationException("An employee cannot be transferred to the same department.");
            }

            if(IsActive == false)
            {
                throw new InvalidOperationException("Cannot transfer a terminated employee.");
            }

            AssignDepartment(newDepartmentId);
        }



    }
}
