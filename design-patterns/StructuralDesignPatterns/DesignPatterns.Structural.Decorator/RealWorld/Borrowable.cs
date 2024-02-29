namespace DesignPatterns.Structural.Decorator.RealWorld;
public class Borrowable : Decorator {
    protected readonly List<String> borrowers = [];

    public Borrowable(LibraryItem libraryItem) : base(libraryItem) { }

    public void BorrowItem(String name) {
        this.borrowers.Add(name);
        this.libraryItem.NumCopies--;
    }

    public void ReturnItem(String name) {
        this.borrowers.Remove(name);
        this.libraryItem.NumCopies++;
    }

    public override void Display() {
        base.Display();
        this.borrowers.ForEach(borrower => Console.WriteLine($"borrower: {borrower}"));
    }
}