using System.Text;

namespace DesignPatterns.Structural.Flyweight;
internal class Page {
    private readonly Int32 no;
    private readonly List<Line> lines;

    public Page(Int32 no, Int32 numberOfLine) {
        this.no = no;
        this.lines = new(numberOfLine);
    }

    public void Add(Line line) {
        this.lines.Add(line);
    }

    public List<Line> getLines() {
        return this.lines;
    }

    public override String ToString() {
        StringBuilder stringBuilder = new();
        foreach(Line line in this.lines) {
            stringBuilder.Append(line);
        }
        return stringBuilder.ToString();
    }
}