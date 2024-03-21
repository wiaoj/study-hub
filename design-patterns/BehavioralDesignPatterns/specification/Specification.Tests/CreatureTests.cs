using Specification.Creature;
using Specification.Property;

namespace Specification.Tests;
public sealed class CreatureTests {
    [Theory]
    [MemberData(nameof(DataProvider))]
    public void Creature_HasExpectedProperties(ICreature testedCreature,
                                               String expectedName,
                                               Size expectedSize,
                                               Movement expectedMovement,
                                               Color expectedColor,
                                               Mass expectedMass) {
        Assert.Equal(expectedName, testedCreature.Name);
        Assert.Equal(expectedSize, testedCreature.Size);
        Assert.Equal(expectedMovement, testedCreature.Movement);
        Assert.Equal(expectedColor, testedCreature.Color);
        Assert.Equal(expectedMass, testedCreature.Mass);
    }

    public static IEnumerable<Object[]> DataProvider() {
        yield return new Object[] { new Dragon(), "Dragon", Size.Large, Movement.Flying, Color.Red, new Mass(39300.0) };
        yield return new Object[] { new Goblin(), "Goblin", Size.Small, Movement.Walking, Color.Green, new Mass(30.0) };
        yield return new Object[] { new KillerBee(), "KillerBee", Size.Small, Movement.Flying, Color.Light, new Mass(6.7) };
        yield return new Object[] { new Octopus(), "Octopus", Size.Normal, Movement.Swimming, Color.Dark, new Mass(12.0) };
        yield return new Object[] { new Shark(), "Shark", Size.Normal, Movement.Swimming, Color.Light, new Mass(500.0) };
        yield return new Object[] { new Troll(), "Troll", Size.Large, Movement.Walking, Color.Dark, new Mass(4000.0) };
    }
}