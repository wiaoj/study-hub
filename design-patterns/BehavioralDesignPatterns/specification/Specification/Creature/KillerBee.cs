using Specification.Property;

namespace Specification.Creature;
public sealed class KillerBee : AbstractCreature {
    private const Double DefaultMass = 6.7D;

    public KillerBee() : this(new Mass(DefaultMass)) { }
    public KillerBee(Mass mass) : base(nameof(KillerBee), Size.Small, Movement.Flying, Color.Light, mass) { }
}