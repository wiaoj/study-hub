namespace DesignPatterns.FactoryMethod.Solution1;
public class PayrollOffice {
    public void PaySalary(Employee employee) {
        String name = employee.Name;
        String department = employee.Department;
        Decimal salary = employee.CalculateSalary();

        Console.WriteLine($"Paying {salary} to {name} in {department}");
    }
}