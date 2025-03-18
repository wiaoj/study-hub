namespace DesignPatterns.Behavioral.Strategy.Structural;
public class Context {
    private readonly Strategy strategy;

    public Context(Strategy strategy) {
        this.strategy = strategy;
    }

    public void ContextInterface() {
        this.strategy.AlgorithmInterface();
    }
}