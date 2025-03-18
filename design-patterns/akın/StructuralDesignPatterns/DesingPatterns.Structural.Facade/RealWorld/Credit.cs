namespace DesingPatterns.Structural.Facade.RealWorld;
public class Credit {
    public Boolean HasGoodCredit(Customer customer) {
        Console.WriteLine("Check credit for {customer.Name}");
        return true;
    }
}