namespace DesignPatterns.Creational.FactoryMethod.Problem;
public sealed class PayrollOffice {
    public void PaySalary(Employee employee) {
        String type = employee.Type;
        String name = employee.Name;
        String department = employee.Department;
        Decimal salary = employee.CalculateSalary();

        Console.WriteLine($"Paying {salary} to {type} {name} in {department}");
    }
}