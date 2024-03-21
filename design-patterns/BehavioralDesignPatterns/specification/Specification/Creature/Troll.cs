using Specification.Property;

namespace Specification.Creature;
public sealed class Troll : AbstractCreature {
    private const Double DefaultMass = 4000.0D;

    public Troll() : this(new Mass(DefaultMass)) { }
    public Troll(Mass mass) : base(nameof(Troll), Size.Large, Movement.Walking, Color.Dark, mass) { }
}