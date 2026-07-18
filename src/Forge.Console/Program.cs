namespace Forge.Console
{
    using static System.Console;
    using Forge.Domain.Entities;
    public class Program
    {
       public static void Main(string[] args)
        {
            Department department = new Department("Finance", "Handles all engineering tasks");
            Position softwareEngineer = new Position("Software Engineer");
            Position customerCareRep = new Position("Customer Care Representative");

            Employee entryLevel = new Employee("John Doe", department.Id, softwareEngineer.Id);
             // Adding a bonus to the salary
            entryLevel.AddCompensation(CompensationType.Salary, new Money(50000, "NGN"), DateTime.UtcNow);
            entryLevel.AddCompensation(CompensationType.Allowance, new Money(1000, "NGN"), DateTime.UtcNow);
            WriteLine($"Employee: {entryLevel.Name}, Department: {department.Name}, Compensation: {string.Join(", ", entryLevel.Compensations.Select(c => $"{c.Type} - {c.Money.Amount} {c.Money.Currency}"))}, Position: {entryLevel.PositionId}, Active: {entryLevel.IsActive}");
           

           
           Employee intern = new Employee("Jane Smith", department.Id, customerCareRep.Id);

           intern.AddCompensation(CompensationType.Stipend, new Money(5000, "NGN"), DateTime.UtcNow);
           WriteLine($"Employee: {intern.Name}, Department: {department.Name}, Compensation: {string.Join(", ", intern.Compensations.Select(c => $"{c.Type} - {c.Money.Amount} {c.Money.Currency}"))}, Position: {intern.PositionId}, Active: {intern.IsActive}");
        }
    }
}
