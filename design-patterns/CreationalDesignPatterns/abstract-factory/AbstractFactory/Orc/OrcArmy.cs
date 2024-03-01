namespace AbstractFactory.Orc;
public sealed class OrcArmy : IArmy {
    public Int32 Size => AbstractFactoryConstants.Orc.ArmySize;

    internal OrcArmy() { }
}