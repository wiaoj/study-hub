using Specification.Creature;
using Specification.Property;
using Specification.Specifications.Bases;

namespace Specification.Specifications;
public sealed class MassGreaterThanSpecification : Specification<ICreature> {
    private readonly Mass mass;

    public MassGreaterThanSpecification(Double mass) : this(new Mass(mass)) { }
    public MassGreaterThanSpecification(Mass mass) {
        this.mass = mass;
    }

    public override Boolean IsSatisfiedBy(ICreature item) {
        return item.Mass.GreaterThan(this.mass);
    }
}