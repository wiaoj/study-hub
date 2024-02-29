namespace DesignPatterns.Structural.Decorator.RealWorld;
public class Book : LibraryItem {
    private readonly String author;
    private readonly String title;

    public Book(String author, String title, Int32 numCopies) {
        this.author = author;
        this.title = title;
        this.NumCopies = numCopies;
    }

    public override void Display() {
        Console.WriteLine("\nBook ------ ");
        Console.WriteLine($"Author: {this.author}");
        Console.WriteLine($"Title: {this.title}");
        Console.WriteLine($"# Copies: {this.NumCopies}");
    }
}