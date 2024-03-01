namespace AbstractFactory.Elf;
public sealed class ElfArmy : IArmy {
    public Int32 Size => AbstractFactoryConstants.Elf.ArmySize;

    internal ElfArmy() { }
}