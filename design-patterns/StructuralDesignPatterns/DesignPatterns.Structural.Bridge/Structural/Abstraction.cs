namespace DesignPatterns.Structural.Bridge.Structural;
public class Abstraction {
    protected Implementor implementor;

    public void SetImplementor(Implementor value) {
        this.implementor = value;
    }

    public virtual void Operation() {
        this.implementor.Operation();
    }
}