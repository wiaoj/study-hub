namespace DesignPatterns.Creational.BuilderPattern.Method2;
public class InternalEmployeeBuilder : EmployeeBuilderM2 {
    public override void SetEmail(String email) {
        // wiaoj@gmail.com
        String emailValue = email.Split('@')[0];
        // endswith @company.com.tr

        this.Employee.Email = $"{emailValue}@company.com.tr";
    }

    public override void SetFullName(String fullName) {
        String[] array = fullName.Split(new[] { ' ', '_', '.' });

        this.Employee.FirstName = array[0];
        this.Employee.LastName = array[1];
    }

    public override void SetUserName(String userName) {
        throw new NotImplementedException();
    }
}