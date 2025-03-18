namespace Visitor;
public class CommanderVisitor : IUnitVisitor {
    public void Visit(Soldier soldier) { }

    public void Visit(Sergeant sergeant) { }

    public void Visit(Commander commander) {
        Func<String> action = () => "Good to see you Commander";
    }
}