using Ambassador.Providers;

namespace Ambassador;
public class RemoteService : IRemoteService {
    private const Int32 Threshold = 200;
    private readonly RandomProvider randomProvider;
    private static readonly Lazy<RemoteService> service = new(() => new RemoteService(), true);
    public static RemoteService Service => service.Value;

    private RemoteService() : this(() => Random.Shared.NextDouble()) { }

    public RemoteService(RandomProvider randomProvider) {
        this.randomProvider = randomProvider;
    }

    public Int64 DoRemoteFunction(Int32 value) {
        Int64 waitTime = (Int64)Math.Floor(this.randomProvider() * 1000);

        try {
            Thread.Sleep((Int32)waitTime);
        }
        catch(ThreadInterruptedException) {
            Thread.CurrentThread.Interrupt();
        }
        return waitTime <= Threshold ? value * 10 : RemoteServiceStatus.Failure.StatusCode();
    }
}