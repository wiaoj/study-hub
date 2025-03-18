using System.Text;

namespace DesignPatterns.Structural.Flyweight;
internal class Book {
    private readonly String name;
    private readonly List<Page> pages;

    private static readonly Character endOfPage = new(12);

    public List<Page> Pages => this.pages;

    public Book(String name, Int32 numberOfPages) {
        this.name = name;
        this.pages = new List<Page>(numberOfPages);
    }

    public void AddPage(Page page) {
        this.pages.Add(page);
    }

    public override String ToString() {
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine(this.name);

        foreach(Page page in this.pages) {
            stringBuilder.Append(page);
            stringBuilder.Append(endOfPage);
        }

        return stringBuilder.ToString();
    }
}