namespace Visitor;
public abstract class Unit {
    private readonly Unit[] children;

    protected Unit(params Unit[] children) {
        this.children = children;
    }

    public virtual void Accept(IUnitVisitor visitor) {
        foreach(Unit child in this.children)
            child.Accept(visitor);
    }
}