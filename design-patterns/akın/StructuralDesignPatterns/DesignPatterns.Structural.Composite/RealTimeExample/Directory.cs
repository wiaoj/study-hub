namespace DesignPatterns.Structural.Composite.RealTimeExample;
public class Directory : FileSystemItem {
    private readonly List<FileSystemItem> childrens = [];

    public Directory(String name) : base(name) { }

    public void AddComponent(FileSystemItem node) {
        this.childrens.Add(node);
    }

    public void RemoveComponent(FileSystemItem node) {
        this.childrens.Remove(node);
    }

    public override Decimal GetSizeInKB() {
        return this.childrens.Sum(x => {
            return x.GetSizeInKB();
        });
    }
}