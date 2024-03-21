using Specification.Property;

namespace Specification.Creature;
public sealed class Shark : AbstractCreature {
    private const Double DefaultMass = 500.0D;

    public Shark() : this(new Mass(DefaultMass)) { }
    public Shark(Mass mass) : base(nameof(Shark), Size.Normal, Movement.Swimming, Color.Light, mass) { }
}