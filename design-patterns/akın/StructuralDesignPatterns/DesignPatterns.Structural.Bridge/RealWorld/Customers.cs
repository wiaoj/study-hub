namespace DesignPatterns.Structural.Bridge.RealWorld;
public class Customers : CustomersBase {
    public override void ShowAll() {
        Console.WriteLine();
        Console.WriteLine(new String('-', 20));
        base.ShowAll();
        Console.WriteLine(new String('-', 20));
    }
}