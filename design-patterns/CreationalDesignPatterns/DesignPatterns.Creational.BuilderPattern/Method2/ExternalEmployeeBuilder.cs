namespace DesignPatterns.BuilderPattern.Method2;
public class ExternalEmployeeBuilder : EmployeeBuilderM2 {
    public override void SetEmail(String email) {
        throw new NotImplementedException();
    }

    public override void SetFullName(String fullName) {
        throw new NotImplementedException();
    }

    public override void SetUserName(String userName) {
        throw new NotImplementedException();
    }
}