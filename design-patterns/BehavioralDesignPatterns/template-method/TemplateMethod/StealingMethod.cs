namespace TemplateMethod;
public abstract class StealingMethod {
    protected internal abstract String PickTarget();
    protected internal abstract void ConfuseTarget(String target);
    protected internal abstract void StealTheItem(String target);

    public void Steal() {
        String target = PickTarget();
        ConfuseTarget(target);
        StealTheItem(target);
    }
}