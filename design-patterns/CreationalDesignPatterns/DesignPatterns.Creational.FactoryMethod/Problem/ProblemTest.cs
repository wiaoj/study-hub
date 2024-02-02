namespace DesignPatterns.FactoryMethod.Problem;
internal static class ProblemTest {
    public static void Run() {
        HR hr = new();
        PayrollOffice payrollOffice = new();

        // Add more employees
        hr.AddNewEmployee(5, "Sami", 2, "Production", "Employee", null, 0);
        hr.AddNewEmployee(6, "Ozlem", 4, "Production", "Employee", null, 0);

        // Now pay time!
        List<Employee> employees = hr.GetEmployees();

        foreach(Employee employee in employees)
            payrollOffice.PaySalary(employee);

    }
}