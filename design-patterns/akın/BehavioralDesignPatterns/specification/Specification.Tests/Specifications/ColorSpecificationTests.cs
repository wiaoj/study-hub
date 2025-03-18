using NSubstitute;
using Specification.Creature;
using Specification.Property;
using Specification.Specifications;

namespace Specification.Tests.Specifications;
public sealed class ColorSpecificationTests {
    [Fact]
    public void TestColor() {
        ICreature greenCreature = Substitute.For<ICreature>();
        greenCreature.Color.Returns(Color.Green);

        ICreature redCreature = Substitute.For<ICreature>();
        redCreature.Color.Returns(Color.Red);

        ColorSpecification greenSpecification = new(Color.Green);

        Assert.True(greenSpecification.IsSatisfiedBy(greenCreature));
        Assert.False(greenSpecification.IsSatisfiedBy(redCreature));
    }
}