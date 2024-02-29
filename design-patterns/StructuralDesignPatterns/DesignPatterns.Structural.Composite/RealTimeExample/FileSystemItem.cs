namespace DesignPatterns.Structural.Composite.RealTimeExample;
public abstract class FileSystemItem {
    public String Name { get; }

    protected FileSystemItem(String name) {
        this.Name = name;
    }

    public abstract Decimal GetSizeInKB();

    public override String ToString() {
        return $"{this.Name} - {GetSizeInKB():##.00} KB";
    }
}