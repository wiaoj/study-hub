namespace DesignPatterns.Creational.AbstractFactory;
public sealed class Button : IComponent {
    public void Paint() {
        Console.WriteLine("Painting a button!");
    }
}