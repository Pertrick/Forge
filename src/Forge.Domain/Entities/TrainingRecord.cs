using Forge.Domain.Common;

namespace Forge.Domain.Entities
{

    public enum GradeType{
        Pass,
        Average,
        Fail
    }

    // A TrainingRecord is a child entity of the Employee aggregate: an employee owns its
    // training records. Because it lives inside the aggregate, it carries NO EmployeeId —
    // containment already expresses the "belongs to this employee" relationship. It still
    // references the Course by identity (CourseId), since Course is a separate aggregate.
    // The constructor is internal so only the Employee root can create one.
    public sealed class TrainingRecord : Entity
    {
        public Guid CourseId { get; private set; }
        public DateTime CompletionDate { get; private set; }
        public GradeType Grade { get; private set; }

        internal TrainingRecord(Guid courseId, GradeType grade, DateTime? completionDate = null)
        {
            AssignCourse(courseId);
            SetGrade(grade);
            SetCompletionDate(completionDate ?? DateTime.UtcNow);
        }

        private void AssignCourse(Guid courseId)
        {
            if (courseId == Guid.Empty)
            {
                throw new ArgumentException("Course ID cannot be empty.", nameof(courseId));
            }

            CourseId = courseId;
        }

        private void SetGrade(GradeType grade)
        {
            Grade = grade;
        }

        private void SetCompletionDate(DateTime completionDate)
        {
            if (completionDate > DateTime.UtcNow)
            {
                throw new ArgumentException("Completion date cannot be in the future.", nameof(completionDate));
            }

            CompletionDate = completionDate;
        }
    }
}
