namespace DesignPatterns.Creational.Singleton;
public sealed class BillPughSingleton : IPrintableName {
    private static Int32 instanceCount = 0;

    private BillPughSingleton() {
        instanceCount++;
    }

    public static BillPughSingleton GetInstance() {
        return SingletonHelper.INSTANCE;
    }

    public Int32 InstanceCount => instanceCount;

    public String Name => $"{nameof(BillPughSingleton)} instance: {instanceCount}";

    private static class SingletonHelper { //Thread-safe 
        internal static readonly BillPughSingleton INSTANCE = new();
        static SingletonHelper() { }
    }
}