namespace DesignPatterns.Singleton;
public sealed class DoubleCheckedLockingSingleton : IPrintableName {
    private static volatile DoubleCheckedLockingSingleton? instance;
    private static Int32 instanceCount = 0;
    private static readonly Object lockObject = new();

    private DoubleCheckedLockingSingleton() {
        instanceCount++;
    }

    public static DoubleCheckedLockingSingleton Instance {
        get {
            if(instance is null)  //Check 1
                lock(lockObject)
                    instance ??= new(); //Check 2 & set instance
            return instance;
        }
    }

    public Int32 InstanceCount => instanceCount;
    public String Name => $"{nameof(DoubleCheckedLockingSingleton)} instance: {instanceCount}";
}