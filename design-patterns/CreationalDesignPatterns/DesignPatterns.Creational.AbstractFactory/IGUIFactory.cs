namespace DesignPatterns.AbstractFactory;
public interface IGUIFactory {
    IComponent CreateButton();

    IComponent CreateList();

    IComponent CreateTable();
}