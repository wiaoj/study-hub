namespace DesingPatterns.Structural.Facade.RealWorld;
public sealed class Customer {
    private readonly String name;
    public String Name => this.name;

    public Customer(String name) {
        this.name = name;
    }
}