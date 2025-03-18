namespace DesignPatterns.Creational.FactoryMethod.Solution1;

public class EmployeeFactory : IFactory {
    public Employee Create() {
        Employee employee = new(Random.Shared.CreateEmployeeId(), Random.Shared.CreateEmployeeName(),
            Random.Shared.CreateEmployeeYear(), Random.Shared.CreateEmployeeDepartment());
        return employee;
    }
}
