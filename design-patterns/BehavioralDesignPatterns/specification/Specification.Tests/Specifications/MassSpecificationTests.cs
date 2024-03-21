using NSubstitute;
using Specification.Creature;
using Specification.Property;
using Specification.Specifications;

namespace Specification.Tests.Specifications;
public sealed class MassSpecificationTests {
    [Fact]
    public void TestMass() {
        ICreature fiftyMassCreature = Substitute.For<ICreature>();
        fiftyMassCreature.Mass.Returns(new Mass(50.0));

        ICreature twoThousandMassCreature = Substitute.For<ICreature>();
        twoThousandMassCreature.Mass.Returns(new Mass(2500.0));

        MassSmallerThanOrEqSpecification massSmallerThanOrEqSpecification = new(500);

        Assert.True(massSmallerThanOrEqSpecification.IsSatisfiedBy(fiftyMassCreature));
        Assert.False(massSmallerThanOrEqSpecification.IsSatisfiedBy(twoThousandMassCreature));
    }
}