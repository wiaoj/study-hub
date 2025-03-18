namespace Visitor;
public class Soldier(params Unit[] children) : Unit(children) {
    public override void Accept(IUnitVisitor visitor) {
        visitor.Visit(this);
        base.Accept(visitor);
    }

    public override String ToString() {
        return nameof(Soldier);
    }
}