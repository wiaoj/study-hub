namespace AbstractFactory.Elf;
public sealed class ElfKing : IKing {
    public String Name => AbstractFactoryConstants.Elf.KingName;

    internal ElfKing() { }
}