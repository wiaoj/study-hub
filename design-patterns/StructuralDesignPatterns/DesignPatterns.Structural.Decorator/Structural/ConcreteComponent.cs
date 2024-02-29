namespace DesignPatterns.Structural.Decorator.Structural;
internal class ConcreteComponent : Component {
    public override void Operation() {
        Console.WriteLine("ConcreteComponent.Operation()");
    }
}