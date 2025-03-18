namespace DesingPatterns.Structural.Facade.RealWorld;
public sealed class Bank {
    public Boolean HasSufficientSavings(Customer customer, Int32 amount) {
        Console.WriteLine($"Check bank for {customer.Name}");
        return true;
    }
}