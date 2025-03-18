namespace DesignPatterns.Creational.Singleton;
public sealed class ThreadSafeLazySingleton2 : IPrintableName {
    // use this or double checked lock object
    private static readonly Lazy<ThreadSafeLazySingleton2> instance = new(() => new ThreadSafeLazySingleton2());
    private static Int32 instanceCount = 0;

    private ThreadSafeLazySingleton2() {
        instanceCount++;
    }

    public static ThreadSafeLazySingleton2 Instance => instance.Value;

    public Int32 InstanceCount => instanceCount;
    public String Name => $"{nameof(ThreadSafeLazySingleton2)} instance: {instanceCount}";
}