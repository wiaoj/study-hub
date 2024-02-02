namespace DesignPatterns.AbstractFactory;
public sealed class GUIFactory : IGUIFactory {
    public IComponent CreateButton() {
        return new Button();
    }

    public IComponent CreateList() {
        return new List();
    }

    public IComponent CreateTable() {
        return new Table();
    }
}