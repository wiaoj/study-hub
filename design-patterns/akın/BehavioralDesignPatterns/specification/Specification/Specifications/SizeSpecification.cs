using Specification.Creature;
using Specification.Property;
using Specification.Specifications.Bases;

namespace Specification.Specifications;
public sealed class SizeSpecification : Specification<ICreature> {
    private readonly Size size;

    public SizeSpecification(Size size) {
        this.size = size;
    }

    public override Boolean IsSatisfiedBy(ICreature item) {
        return item.Size.Equals(this.size);
    }
}