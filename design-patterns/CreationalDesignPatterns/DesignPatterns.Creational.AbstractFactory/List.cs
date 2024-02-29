namespace DesignPatterns.Creational.AbstractFactory;
public sealed class List : IComponent {
    public void Paint() {
        Console.WriteLine("Painting a list!");
    }
}