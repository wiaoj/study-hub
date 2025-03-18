namespace Specification.Property;
public readonly struct Size {
    public static readonly Size Small = new(nameof(Small));
    public static readonly Size Normal = new(nameof(Normal));
    public static readonly Size Large = new(nameof(Large));

    private readonly String title;
    public readonly String Title => this.title;
    private Size(String title) {
        this.title = title;
    }

    public override String ToString() {
        return this.title;
    }
}