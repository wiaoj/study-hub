namespace AbstractFactory.Orc;
public sealed class OrcKing : IKing {
    public String Name => AbstractFactoryConstants.Orc.KingName;

    internal OrcKing() { }
}