namespace DesignPatterns.Structural.Bridge.Structural;
public class RefinedAbstraction : Abstraction {
    public override void Operation() {
        this.implementor.Operation();
    }
}