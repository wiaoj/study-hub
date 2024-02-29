namespace DesignPatterns.Creational.AbstractFactory;
public sealed class Client {
    public Client(IGUIFactory factory) {
        IComponent button = factory.CreateButton();
        button.Paint();

        IComponent list = factory.CreateList();
        list.Paint();

        IComponent table = factory.CreateTable();
        table.Paint();
    }
}