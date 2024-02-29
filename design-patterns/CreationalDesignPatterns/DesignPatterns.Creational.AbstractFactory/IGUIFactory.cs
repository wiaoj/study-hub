namespace DesignPatterns.Creational.AbstractFactory;
public interface IGUIFactory {
    IComponent CreateButton();

    IComponent CreateList();

    IComponent CreateTable();
}