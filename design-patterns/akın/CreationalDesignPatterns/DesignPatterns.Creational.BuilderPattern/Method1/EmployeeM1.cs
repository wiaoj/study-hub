namespace DesignPatterns.Creational.BuilderPattern.Method1;
public class EmployeeM1 {
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public String Email { get; set; }
    public String UserName { get; set; }

    public override String ToString() {
        return $"{this.FirstName} {this.LastName} ({this.UserName})";
    }
}