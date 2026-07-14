namespace Forge.Console
{
    using static System.Console;
    using Forge.Domain.Entities;
    public class Program
    {
       public static void Main(string[] args)
        {
            Department department = new Department("Finance", "Handles all engineering tasks");
           
            Employee entryLevel = new Employee("John Doe", department.Id);
             // Adding a bonus to the salary
            entryLevel.AddCompensation(new Compensation(CompensationType.Salary, new Money(50000, "NGN"), DateTime.UtcNow));
            entryLevel.AddCompensation(new Compensation(CompensationType.Allowance, new Money(1000, "NGN"), DateTime.UtcNow));
            WriteLine($"Employee: {entryLevel.Name}, Department: {department.Name}, Compensation: {string.Join(", ", entryLevel.Compensations.Select(c => $"{c.Type} - {c.Money.Amount} {c.Money.Currency}"))}, Active: {entryLevel.IsActive}");
           

           
           Employee intern = new Employee("Jane Smith", department.Id);
           intern.AddCompensation(new Compensation(CompensationType.Stipend, new Money(5000, "NGN"), DateTime.UtcNow));
           WriteLine($"Employee: {intern.Name}, Department: {department.Name}, Compensation: {string.Join(", ", intern.Compensations.Select(c => $"{c.Type} - {c.Money.Amount} {c.Money.Currency}"))}, Active: {intern.IsActive}");
        }
    }
}
