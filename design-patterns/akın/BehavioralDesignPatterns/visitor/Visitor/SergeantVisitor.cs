namespace Visitor;
public class SergeantVisitor : IUnitVisitor {
    public void Visit(Soldier soldier) { }

    public void Visit(Sergeant sergeant) {
        Func<String> action = () => "Hello Sergeant";
    }

    public void Visit(Commander commander) { }
}