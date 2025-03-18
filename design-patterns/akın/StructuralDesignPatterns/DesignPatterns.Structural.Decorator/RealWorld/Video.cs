namespace DesignPatterns.Structural.Decorator.RealWorld;
public class Video : LibraryItem {
    private readonly String director;
    private readonly String title;
    private readonly Int32 playTime;

    public Video(String director, String title, Int32 numCopies, Int32 playTime) {
        this.director = director;
        this.title = title;
        this.NumCopies = numCopies;
        this.playTime = playTime;
    }

    public override void Display() {
        Console.WriteLine("\nVideo ----- ");
        Console.WriteLine($"Director: {this.director}");
        Console.WriteLine($"Title: {this.title}");
        Console.WriteLine($"# Copies: {this.NumCopies}");
        Console.WriteLine($"Playtime: {this.playTime}\n");
    }
}