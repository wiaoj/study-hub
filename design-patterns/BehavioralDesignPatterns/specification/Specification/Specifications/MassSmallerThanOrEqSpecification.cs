using Specification.Creature;
using Specification.Property;
using Specification.Specifications.Bases;

namespace Specification.Specifications;
public sealed class MassSmallerThanOrEqSpecification : Specification<ICreature> {
    private readonly Mass mass;

    public MassSmallerThanOrEqSpecification(Double mass) : this(new Mass(mass)) { }
    public MassSmallerThanOrEqSpecification(Mass mass) {
        this.mass = mass;
    }

    public override Boolean IsSatisfiedBy(ICreature item) {
        return item.Mass.SmallerThanOrEq(this.mass);
    }
}