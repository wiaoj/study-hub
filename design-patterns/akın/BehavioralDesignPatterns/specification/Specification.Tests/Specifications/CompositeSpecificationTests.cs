using NSubstitute;
using Specification.Creature;
using Specification.Property;
using Specification.Specifications;
using Specification.Specifications.Bases;

namespace Specification.Tests.Specifications;
public sealed class CompositeSpecificationTests {
    [Fact]
    public void AndSpecificationTest() {
        ICreature swimmingHeavyCreature = CreateCreature(Movement.Swimming, 100.0);
        ICreature swimmingLightCreature = CreateCreature(Movement.Swimming, 25.0);

        Specification<ICreature> lightOrSwimmingSpecification = new MassSmallerThanOrEqSpecification(50.0)
            .And(new MovementSpecification(Movement.Swimming));

        Assert.False(lightOrSwimmingSpecification.IsSatisfiedBy(swimmingHeavyCreature));
        Assert.True(lightOrSwimmingSpecification.IsSatisfiedBy(swimmingLightCreature));
    }

    [Fact]
    public void OrSpecificationTest() {
        ICreature swimmingHeavyCreature = CreateCreature(Movement.Swimming, 100.0);
        ICreature swimmingLightCreature = CreateCreature(Movement.Swimming, 25.0);

        Specification<ICreature> lightOrSwimmingSpecification = new MassSmallerThanOrEqSpecification(50.0)
            .Or(new MovementSpecification(Movement.Swimming));

        Assert.True(lightOrSwimmingSpecification.IsSatisfiedBy(swimmingHeavyCreature));
        Assert.True(lightOrSwimmingSpecification.IsSatisfiedBy(swimmingLightCreature));
    }

    [Fact]
    public void NotSpecificationTest() {
        ICreature swimmingHeavyCreature = CreateCreature(Movement.Swimming, 100.0);
        ICreature swimmingLightCreature = CreateCreature(Movement.Swimming, 25.0);

        Specification<ICreature> lightOrSwimmingSpecification = new MassSmallerThanOrEqSpecification(50.0)
            .Not();

        Assert.True(lightOrSwimmingSpecification.IsSatisfiedBy(swimmingHeavyCreature));
        Assert.False(lightOrSwimmingSpecification.IsSatisfiedBy(swimmingLightCreature));
    }

    private ICreature CreateCreature(Movement movement, Double mass) {
        ICreature creature = Substitute.For<ICreature>();
        creature.Movement.Returns(movement);
        creature.Mass.Returns(new Mass(mass));
        return creature;
    }
}