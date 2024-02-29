namespace DesignPatterns.Structural.Decorator.Structural;
public class ConcreteDecoratorA : Decorator {
    public override void Operation() {
        base.Operation();
        Console.WriteLine("ConcreteDecoratorA.Operation()");
    }
}