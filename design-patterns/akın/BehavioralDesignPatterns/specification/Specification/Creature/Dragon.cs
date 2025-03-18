using Specification.Property;

namespace Specification.Creature;
public sealed class Dragon : AbstractCreature {
    private const Double DefaultMass = 39300.0D;

    public Dragon() : this(new Mass(DefaultMass)) { }
    public Dragon(Mass mass) : base(nameof(Dragon), Size.Large, Movement.Flying, Color.Red, mass) { }
}