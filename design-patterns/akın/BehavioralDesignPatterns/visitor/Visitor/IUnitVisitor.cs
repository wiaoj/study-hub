namespace Visitor;
public interface IUnitVisitor {
    void Visit(Soldier soldier);
    void Visit(Sergeant sergeant);
    void Visit(Commander commander);
}