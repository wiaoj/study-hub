namespace DesignPatterns.Structural.Composite.BasicComposite;
public class Composite : IComponent {
    public String Name { get; set; }
    private readonly List<IComponent> components = [];

    public Composite(String name) {
        this.Name = name;
    }

    public void AddComponent(IComponent component) {
        this.components.Add(component);
    }

    public void DisplayPrice() {
        foreach(IComponent component in this.components) {
            component.DisplayPrice();
        }
    }
}