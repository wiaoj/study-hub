namespace DesignPatterns.Structural.Decorator.RealWorld;
public abstract class LibraryItem {
    public Int32 NumCopies { get; set; }
    public abstract void Display();
}