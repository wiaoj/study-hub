namespace DesignPatterns.Behavioral.Strategy.Structural;
public class ConcreteStrategyA : Strategy {
    public override void AlgorithmInterface() {
        Console.WriteLine("Called ConcreteStrategyA.AlgorithmInterface()");
    }
}