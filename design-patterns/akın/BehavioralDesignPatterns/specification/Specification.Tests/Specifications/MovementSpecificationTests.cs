using NSubstitute;
using Specification.Creature;
using Specification.Property;
using Specification.Specifications;

namespace Specification.Tests.Specifications;
public sealed class MovementSpecificationTests {
    [Fact]
    public void TestMovement() {
        ICreature swimmingCreature = Substitute.For<ICreature>();
        swimmingCreature.Movement.Returns(Movement.Swimming);

        ICreature flyingCreature = Substitute.For<ICreature>();
        flyingCreature.Movement.Returns(Movement.Flying);

        MovementSpecification swimmingSpecification = new(Movement.Swimming);

        Assert.True(swimmingSpecification.IsSatisfiedBy(swimmingCreature));
        Assert.False(swimmingSpecification.IsSatisfiedBy(flyingCreature));
    }
}