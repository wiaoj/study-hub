namespace DesignPatterns.Creational.Singleton;
public sealed class LazySingleton : IPrintableName {
    private static LazySingleton? instance;
    private static Int32 instanceCount = 0;

    private LazySingleton() {
        instanceCount++;
    }

    public static LazySingleton Instance {
        get {
            instance ??= new();
            // or if(instance is null) instance = new();
            return instance;
        }
    }

    public Int32 InstanceCount => instanceCount;
    public String Name => $"{nameof(LazySingleton)} instance: {instanceCount}";
}