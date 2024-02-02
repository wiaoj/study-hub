namespace DesignPatterns.Singleton;
public sealed class Singleton : IPrintableName {
    private static readonly Singleton instance = new();
    private static Int32 instanceCount = 0;

    private Singleton() {
        instanceCount++;
    }

    public static Singleton GetInstance() {
        return instance;
    }

    public Int32 InstanceCount => instanceCount;

    public String Name => $"{nameof(Singleton)} instance: {instanceCount}";
}