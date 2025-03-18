namespace Visitor;
public class SoldierVisitor : IUnitVisitor {
    public void Visit(Soldier soldier) {
        Func<String> action = () => "Greetings Soldier";
    }

    public void Visit(Sergeant sergeant) { }

    public void Visit(Commander commander) { }
}