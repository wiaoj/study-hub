namespace DesignPatterns.Structural.Composite.RealTimeExample;
public class FileItem : FileSystemItem {
    public Int64 FileBytes { get; }

    public FileItem(String name, Int64 fileBytes) : base(name) {
        this.FileBytes = fileBytes;
    }

    public override Decimal GetSizeInKB() {
        Console.WriteLine($"\tFile Name: {this.Name}");
        return Decimal.Divide(this.FileBytes, 1024);
    }
}