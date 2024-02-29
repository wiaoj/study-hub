namespace DesignPatterns.Structural.Decorator.RealWorld;
public abstract class Decorator : LibraryItem {
    protected LibraryItem libraryItem;

    protected Decorator(LibraryItem libraryItem) {
        this.libraryItem = libraryItem;
    }

    public override void Display() {
        libraryItem.Display();
    }
}