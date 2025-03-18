namespace DesignPatterns.Structural.Decorator.Structural;
public abstract class Decorator : Component {
    protected Component? component;

    public void SetComponent(Component component) {
        this.component = component;
    }

    public override void Operation() {
        this.component?.Operation();
    }
}