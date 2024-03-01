namespace AbstractFactory.Human;
public sealed class HumanArmy : IArmy {
    public Int32 Size => AbstractFactoryConstants.Human.ArmySize;

    internal HumanArmy() { }
}