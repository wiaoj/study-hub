using Specification.Property;

namespace Specification.Creature;
public sealed class Octopus : AbstractCreature {
    private const Double DefaultMass = 12.0D;

    public Octopus() : this(new Mass(DefaultMass)) { }
    public Octopus(Mass mass) : base(nameof(Octopus), Size.Normal, Movement.Swimming, Color.Dark, mass) { }
}