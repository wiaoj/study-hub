using NSubstitute;
using Specification.Creature;
using Specification.Property;
using Specification.Specifications;

namespace Specification.Tests.Specifications;
public sealed class SizeSpecificationTests {
    [Fact]
    public void TestMovement() {
        ICreature normalCreature = Substitute.For<ICreature>();
        normalCreature.Size.Returns(Size.Normal);

        ICreature smallCreature = Substitute.For<ICreature>();
        smallCreature.Size.Returns(Size.Small);

        SizeSpecification normalSpecification = new(Size.Normal);

        Assert.True(normalSpecification.IsSatisfiedBy(normalCreature));
        Assert.False(normalSpecification.IsSatisfiedBy(smallCreature));
    }
}