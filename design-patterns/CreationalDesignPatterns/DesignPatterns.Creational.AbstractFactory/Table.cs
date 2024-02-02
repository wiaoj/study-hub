namespace DesignPatterns.AbstractFactory;
public sealed class Table : IComponent {
    public void Paint() {
        Console.WriteLine("Painting a table!");
    }
}