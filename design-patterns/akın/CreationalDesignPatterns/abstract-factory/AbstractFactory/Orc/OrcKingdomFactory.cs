namespace AbstractFactory.Orc;

public sealed class OrcKingdomFactory : IKingdomFactory {
    internal OrcKingdomFactory() { }

    public IArmy CreateArmy() {
        return new OrcArmy();
    }

    public ICastle CreateCastle() {
        return new OrcCastle();
    }

    public IKing CreateKing() {
        return new OrcKing();
    }
}