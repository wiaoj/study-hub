namespace AbstractFactory.Human;
public sealed class HumanCastle : ICastle {
    public String Description => AbstractFactoryConstants.Human.CastleDescription;

    internal HumanCastle() { }
}