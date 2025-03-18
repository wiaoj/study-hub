namespace DesignPatterns.Creational.FactoryMethod.Solution1;
internal static class SolutionOneTest {
    public static void Run() {
        HR hr = new();
        PayrollOffice payrollOffice = new();

#pragma warning disable CA1859 // Use concrete types when possible for improved performance
        IFactory employeeFactory = new EmployeeFactory();
        IFactory managerFactory = new ManagerFactory();
        IFactory directorFactory = new DirectorFactory();
#pragma warning restore CA1859 // Use concrete types when possible for improved performance

        // Add more employees
        hr.AddNewEmployee(employeeFactory.Create());
        hr.AddNewEmployee(employeeFactory.Create());
        hr.AddNewEmployee(employeeFactory.Create());
        hr.AddNewEmployee(managerFactory.Create());
        hr.AddNewEmployee(managerFactory.Create());
        hr.AddNewEmployee(directorFactory.Create());

        hr.ListEmployees();

        // Now pay time!
        List<Employee> employees = hr.GetEmployees();

        foreach(Employee employee in employees)
            payrollOffice.PaySalary(employee);
    }
}