namespace DesignPatterns.Creational.BuilderPattern.Method1;
public class EmployeeBuilderM1 {
    private EmployeeM1 employee { get; set; }

    public EmployeeBuilderM1() {
        this.employee = new();
    }

    public EmployeeBuilderM1 SetFullName(String fullName) {
        // validations

        String[] array = fullName.Split(' ');

        this.employee.FirstName = array[0];
        this.employee.LastName = array[1];
        return this;
    }

    public EmployeeBuilderM1 SetEmail(String email) {
        // Email validation

        this.employee.Email = email;
        return this;
    }

    public EmployeeBuilderM1 SetUserName(String userName) {
        this.employee.UserName = userName;

        return this;
    }

    public EmployeeM1 BuildEmployee() {

        return this.employee;
    }
}