namespace Specification.Property;
public sealed record Mass {
    private readonly Double value;
    private readonly String title;

    public Mass(Double value) {
        this.value = value;
        this.title = $"{value}kg";
    }

    public Boolean GreaterThan(Mass other) {
        return this.value > other.value;
    }

    public Boolean SmallerThan(Mass other) {
        return this.value < other.value;
    }

    public Boolean GreaterThanOrEq(Mass other) {
        return this.value >= other.value;
    }

    public Boolean SmallerThanOrEq(Mass other) {
        return this.value <= other.value;
    }

    public override String ToString() {
        return this.title;
    }
}