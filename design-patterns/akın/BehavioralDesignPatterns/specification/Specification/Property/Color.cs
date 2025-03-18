namespace Specification.Property;
public readonly struct Color {
    public static readonly Color Dark = new(nameof(Dark));
    public static readonly Color Light = new(nameof(Light));
    public static readonly Color Green = new(nameof(Green));
    public static readonly Color Red = new(nameof(Red));

    private readonly String title;
    public readonly String Title => this.title;
    private Color(String title) {
        this.title = title;
    }

    public override String ToString() {
        return this.title;
    }
}