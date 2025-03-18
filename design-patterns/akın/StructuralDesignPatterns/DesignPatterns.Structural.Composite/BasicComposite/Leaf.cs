namespace DesignPatterns.Structural.Composite.BasicComposite;
public class Leaf : IComponent {
    public Int32 Price { get; set; }
    public String Name { get; set; }

    public Leaf(String name, Int32 price) {
        this.Price = price;
        this.Name = name;
    }

    public void DisplayPrice() {
        Console.WriteLine($"\tComponent Name: {this.Name} and Price: {this.Price}");
    }
}