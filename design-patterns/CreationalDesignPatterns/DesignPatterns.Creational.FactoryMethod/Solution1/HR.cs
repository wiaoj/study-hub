namespace DesignPatterns.FactoryMethod.Solution1;
public class HR {
    private readonly List<Employee> employees = [];

    public List<Employee> GetEmployees() {
        return this.employees;
    }

    public Int32 GetNumberOfEmployees() {
        return this.employees.Count;
    }

    public void AddNewEmployee(Employee employee) {
        this.employees.Add(employee);
    }

    public void ListEmployees() {
        Console.WriteLine("All Employees");
        foreach(Employee employee in this.employees)
            Console.WriteLine(employee);
    }
}