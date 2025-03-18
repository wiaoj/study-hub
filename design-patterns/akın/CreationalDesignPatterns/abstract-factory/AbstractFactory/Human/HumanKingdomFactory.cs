namespace AbstractFactory.Human;
public sealed class HumanKingdomFactory : IKingdomFactory {
    internal HumanKingdomFactory() { }

    public IArmy CreateArmy() {
        return new HumanArmy();
    }

    public ICastle CreateCastle() {
        return new HumanCastle();
    }

    public IKing CreateKing() {
        return new HumanKing();
    }
}