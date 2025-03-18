using Specification.Property;

namespace Specification.Creature;
public interface ICreature {
    String Name { get; }
    Size Size { get; }
    Movement Movement { get; }
    Color Color { get; }
    Mass Mass { get; }
}