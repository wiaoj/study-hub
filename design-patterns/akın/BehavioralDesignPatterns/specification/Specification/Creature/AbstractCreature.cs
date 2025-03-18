using Specification.Property;

namespace Specification.Creature;
public abstract class AbstractCreature : ICreature {
    private readonly String name;
    private readonly Size size;
    private readonly Movement movement;
    private readonly Color color;
    private readonly Mass mass;

    public String Name => this.name;
    public Size Size => this.size;
    public Movement Movement => this.movement;
    public Color Color => this.color;
    public Mass Mass => this.mass;

    protected AbstractCreature(String name,
                               Size size,
                               Movement movement,
                               Color color,
                               Mass mass) {
        this.name = name;
        this.size = size;
        this.movement = movement;
        this.color = color;
        this.mass = mass;
    }
}