namespace DesignPatterns.FactoryMethod.Problem;
public sealed class HR {
    private readonly List<Employee> employees = [];

    public HR() {
        Employee employee = new(1, "Ahmet", 10, "Production", "Employee");
        this.employees.Add(employee);
        employee = new Employee(2, "Zeynep", 3, "Sales", "Employee");
        this.employees.Add(employee);
        employee = new Employee(3, "Kemal", 7, "Production", "Employee");
        this.employees.Add(employee);

        Employee manager = new(20, "Ahmet", 10, "Marketing", "Manager", "Marketing");
        this.employees.Add(manager);
        manager = new Employee(21, "Mehmet", 14, "Production", "Manager", "Production");
        this.employees.Add(manager);

        Employee director = new(30, "Ahmet", 19, "Company", "Director", "Company", 5000);
        this.employees.Add(director);

    }

    public void AddNewEmployee(Int32 no, String name, Int32 year, String department, String type, String departmentManaged, Decimal bonus) {
        Employee employee;
        switch(type) {
            case "Employee":
            employee = new Employee(no, name, year, department, type);
            this.employees.Add(employee);
            break;

            case "Manager":
            employee = new Employee(no, name, year, department, type, departmentManaged);
            this.employees.Add(employee);
            break;

            case "Director":
            employee = new Employee(no, name, year, department, type, departmentManaged, bonus);
            this.employees.Add(employee);
            break;
        }
    }

    public List<Employee> GetEmployees() {
        return this.employees;
    }

    public Int32 GetNumberOfEmployees() {
        return this.employees.Count;
    }
}