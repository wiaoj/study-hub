using System.Diagnostics;

namespace Ambassador;
public class ServiceAmbassador : IRemoteService {
    private const Int32 RETRIES = 3;
    private const Int32 DELAY_MS = 3000;

    public Int64 DoRemoteFunction(Int32 value) {
        return SafeCall(value);
    }

    private Int64 SafeCall(Int32 value) {
        Int32 retries = 0;
        Int64 result = RemoteServiceStatus.Failure.StatusCode();

        for(Int32 i = 0; i < RETRIES; i++) {
            if(retries >= RETRIES) {
                return RemoteServiceStatus.Failure.StatusCode();
            }

            if((result = CheckLatency(value)) == RemoteServiceStatus.Failure.StatusCode()) {

                retries++;

                try {
                    Thread.Sleep(DELAY_MS);
                }
                catch(ThreadInterruptedException) {
                    Thread.CurrentThread.Interrupt();
                }
            }
            else {
                break;
            }
        }

        return result;
    }

    private static Int64 CheckLatency(Int32 value) {
        Stopwatch stopwatch = Stopwatch.StartNew();
        Int64 result = RemoteService.Service.DoRemoteFunction(value);
        stopwatch.Stop();

        Int64 timeTaken = stopwatch.ElapsedMilliseconds;

        return result;
    }
}