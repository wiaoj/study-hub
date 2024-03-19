namespace Proxy;
public class Wizard {
    private readonly String name;
    public String Name => this.name;
    public Wizard(String name) {
        this.name = name;
    }

    public override String ToString() {
        return this.name;
    }
}