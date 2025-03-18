using Specification.Creature;
using Specification.Property;
using Specification.Specifications.Bases;

namespace Specification.Specifications;
public sealed class ColorSpecification : Specification<ICreature> {
    private readonly Color color;

    public ColorSpecification(Color color) {
        this.color = color;
    }

    public override Boolean IsSatisfiedBy(ICreature item) {
        return this.color.Equals(item.Color);
    }
}