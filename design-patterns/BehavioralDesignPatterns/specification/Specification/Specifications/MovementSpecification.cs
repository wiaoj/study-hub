using Specification.Creature;
using Specification.Property;
using Specification.Specifications.Bases;

namespace Specification.Specifications;
public sealed class MovementSpecification : Specification<ICreature> {
    private readonly Movement movement;

    public MovementSpecification(Movement movement) {
        this.movement = movement;
    }

    public override Boolean IsSatisfiedBy(ICreature item) {
        return item.Movement.Equals(this.movement);
    }
}