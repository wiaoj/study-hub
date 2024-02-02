namespace DesignPatterns.BuilderPattern.Method2;
public abstract class EmployeeBuilderM2 : IEmployeeBuilderM2 {
    protected EmployeeM2 Employee { get; set; }

    public EmployeeBuilderM2() {
        this.Employee = new();
    }

    public abstract void SetFullName(String fullName);
    public abstract void SetEmail(String email);
    public abstract void SetUserName(String userName);

    public EmployeeM2 BuildEmployee() {
        return this.Employee;
    }
}