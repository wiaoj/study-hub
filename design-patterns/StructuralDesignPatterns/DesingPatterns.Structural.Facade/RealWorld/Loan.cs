namespace DesingPatterns.Structural.Facade.RealWorld;
public sealed class Loan {
    public Boolean HasNoBadLoans(Customer customer) {
        Console.WriteLine($"Check loans for {customer.Name}");
        return true;
    }
}