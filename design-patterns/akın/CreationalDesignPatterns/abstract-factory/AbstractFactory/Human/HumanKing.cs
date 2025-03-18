namespace AbstractFactory.Human;
public sealed class HumanKing : IKing {
    public String Name => AbstractFactoryConstants.Human.KingName;

    internal HumanKing() { }
}