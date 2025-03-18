namespace Specification.Property;
public readonly struct Movement {
    public static readonly Movement Walking = new(nameof(Walking));
    public static readonly Movement Swimming = new(nameof(Swimming));
    public static readonly Movement Flying = new(nameof(Flying));

    private readonly String title;
    public readonly String Title => this.title;
    private Movement(String title) {
        this.title = title;
    }

    public override String ToString() {
        return this.title;
    }
}