namespace DesignPatterns.FactoryMethod.Solution1;
public class ManagerFactory : IFactory {
    public Employee Create() {
        String department = Random.Shared.CreateEmployeeDepartment();
        Manager manager = new(Random.Shared.CreateEmployeeId(),
                              Random.Shared.CreateEmployeeName(),
                              Random.Shared.CreateEmployeeYear(),
                              department,
                              department);
        return manager;
    }
}