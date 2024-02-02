namespace DesignPatterns.BuilderPattern.Method2;
public interface IEmployeeBuilderM2 {
    public EmployeeM2 BuildEmployee();
    public void SetEmail(String email);
    public void SetFullName(String fullName);
    public void SetUserName(String userName);
}