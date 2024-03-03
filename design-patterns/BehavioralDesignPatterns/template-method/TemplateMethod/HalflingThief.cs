namespace TemplateMethod;
public class HalflingThief {
    private StealingMethod method;

    public HalflingThief(StealingMethod method) {
        this.method = method;
    }

    public void Steal() {
        this.method.Steal();
    }

    public void ChangeMethod(StealingMethod method) {
        this.method = method;
    }
}