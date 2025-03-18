namespace DesignPatterns.Structural.Flyweight;
internal class BookFactory : IFactory {
    private readonly List<Character> arrayList = new();

    public Character CreateCharacter(Char c, Boolean upperCase) {
        return new Character(c, upperCase);
    }

    public Line CreateLine(Int32 numberOfCharacters) {
        return new Line(numberOfCharacters);
    }

    public Page CreatePage(Int32 no, Int32 numberOfLines) {
        // TODO Auto-generated method stub
        return new Page(no, numberOfLines);
    }

    public Book CreateBook(String name, Int32 numberOfPages) {
        return new Book(name, numberOfPages);
    }
}

internal interface IFactory {
    Character CreateCharacter(Char c, Boolean upperCase);

    Line CreateLine(Int32 numberOfCharacters);

    Page CreatePage(Int32 no, Int32 numberOfLines);

    Book CreateBook(String name, Int32 numberOfPages);
}