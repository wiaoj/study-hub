namespace DesignPatterns.Structural.Flyweight;
internal class Character {
    // Intrinsic properties
    private readonly Char value;
    private readonly Boolean upperCase;

    // Extrinsic properties
    private Line line;
    private Int32 position;

    public Character(Char value, Boolean upperCase) {
        this.upperCase = upperCase;
        this.value = value;
    }

    public Character(Int32 value) {
        this.value = (Char)value;
    }

    public Char GetValue() {
        return this.upperCase ? Char.ToUpper(this.value) : Char.ToLower(this.value);
    }

    public Boolean IsUpperCase() {
        return this.upperCase;
    }

    public Line GetLine() {
        return this.line;
    }

    public void SetLine(Line line) {
        this.line = line;
    }

    public Int32 GetPosition() {
        return this.position;
    }

    public void SetPosition(Int32 position) {
        this.position = position;
    }

    public override String ToString() {
        return GetValue().ToString();
    }
}