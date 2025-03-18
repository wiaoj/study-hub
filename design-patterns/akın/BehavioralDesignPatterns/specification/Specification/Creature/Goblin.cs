using Specification.Property;

namespace Specification.Creature;
public sealed class Goblin : AbstractCreature {
    private const Double DefaultMass = 30.0D;

    public Goblin() : this(new Mass(DefaultMass)) { }
    public Goblin(Mass mass) : base(nameof(Goblin), Size.Small, Movement.Walking, Color.Green, mass) { }
}