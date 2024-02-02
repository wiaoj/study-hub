using System.Text;

namespace DesignPatterns.Structural.Flyweight;
internal class Line {
    private readonly List<Character> chars;
    private static readonly Character endOfLine = new('\n');
    private Int32 emptyPosition;
    private Boolean full;
    private readonly Int32 numberOfCharacters = 1;

    public Line(Int32 numberOfCharacters) {
        this.numberOfCharacters = numberOfCharacters;
        this.chars = new(numberOfCharacters);
    }

    public Boolean Add(Character character) {
        if(!this.full) {
            this.chars.Add(character);
            character.SetLine(this);
            character.SetPosition(this.emptyPosition);
            this.emptyPosition++;
            if(this.emptyPosition == this.numberOfCharacters + 1)
                this.full = true;
            return true;
        }
        else
            return false;
    }

    public void AddEndOfLine() {
        this.chars.Add(endOfLine);
    }

    public List<Character> GetChars() {
        return this.chars;
    }

    public override String ToString() {
        StringBuilder stringBuilder = new();

        foreach(Character character in this.chars) {
            stringBuilder.Append(character.GetValue());
        }

        return stringBuilder.ToString();
    }
}
