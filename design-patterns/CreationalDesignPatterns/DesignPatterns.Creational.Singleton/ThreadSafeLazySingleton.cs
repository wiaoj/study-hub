namespace DesignPatterns.Singleton;
public sealed class ThreadSafeLazySingleton : IPrintableName {
    private static ThreadSafeLazySingleton? instance;
    private static Int32 instanceCount = 0;
    private static readonly Object lockObject = new();

    private ThreadSafeLazySingleton() {
        instanceCount++;
    }

    public static ThreadSafeLazySingleton Instance {
        get {
            lock(lockObject) {
                instance ??= new(); 
                return instance;
            }
        }
    }

    public Int32 InstanceCount => instanceCount;
    public String Name => $"{nameof(ThreadSafeLazySingleton)} instance: {instanceCount}";
}