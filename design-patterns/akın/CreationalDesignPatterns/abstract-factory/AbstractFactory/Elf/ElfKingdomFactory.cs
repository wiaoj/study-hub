namespace AbstractFactory.Elf;
public sealed class ElfKingdomFactory : IKingdomFactory {
    internal ElfKingdomFactory() { }

    public IArmy CreateArmy() {
        return new ElfArmy();
    }

    public ICastle CreateCastle() {
        return new ElfCastle();
    }

    public IKing CreateKing() {
        return new ElfKing();
    }
}