using System.Diagnostics;

namespace DesignPatterns.Singleton;
public sealed class Client {
    void PrintInstanceName(IPrintableName printableName) {
        //Console.WriteLine(printableName.Name);
    }

    public void SingletonTests() {
        SingletonTests(true);
    }
    public void SingletonTests(Boolean printInstanceName) {
        Singleton singleton = Singleton.GetInstance();
        LazySingleton lazySingleton = LazySingleton.Instance;

        if(printInstanceName) {
            PrintInstanceName(singleton); // 1    
            PrintInstanceName(lazySingleton); // 1
        }
    }

    public void ThreadedLazy(Int32 count) {
        While(count, () => {
            //SingletonTestMethods();
            //Thread thread = new(new ThreadStart(() => this.SingletonTests(false)));
            Thread thread = new(this.SingletonTests);
            thread.Start();

            /*
             * Singleton instance: 1
             * LazySingleton instance: 3 --> Problem!
             */
        });
    }

    public Int64 ThreadeSafeLazy(Int32 count) {
        While(count, () => {
            Thread thread = new(() => {
                PrintInstanceName(ThreadSafeLazySingleton.Instance);
            });
            thread.Start();

            /*
             * ThreadSafeLazySingleton instance: 1!
             * Problem solved
             */
        });

        var action = () => {
            While(count, () => {
                Thread thread = new(() => {
                    PrintInstanceName(ThreadSafeLazySingleton.Instance);
                });
                thread.Start();

                /*
                 * ThreadSafeLazySingleton instance: 1!
                 * Problem solved
                 */
            });
        };
        return Stopwatch(action, nameof(ThreadSafeLazySingleton));
    }

    public Int64 ThreadeSafeLazy2(Int32 count) {
        While(count, () => {
            Thread thread = new(() => {
                PrintInstanceName(ThreadSafeLazySingleton2.Instance);
            });
            thread.Start();

            /*
             * ThreadSafeLazySingleton2 instance: 1!
             * Problem solved
             */
        });


        var action = () => {
            While(count, () => {
                Thread thread = new(() => {
                    PrintInstanceName(ThreadSafeLazySingleton2.Instance);
                });
                thread.Start();

                /*
                 * ThreadSafeLazySingleton2 instance: 1!
                 * Problem solved
                 */
            });
        };
        return Stopwatch(action, nameof(ThreadSafeLazySingleton2));
    }

    public Int64 DoubleCheckedLocking(Int32 count) {
        var action = () => {
            While(count, () => {
                Thread thread = new(() => {
                    PrintInstanceName(DoubleCheckedLockingSingleton.Instance);
                });
                thread.Start();
            });
        };
        return Stopwatch(action, nameof(DoubleCheckedLockingSingleton));
    }

    void While(Int32 count, Action action) {
        while(count-- > 0) {
            action.Invoke();
        }
    }

    Int64 Stopwatch(Action action, String methodName) {
        return Stopwatch(action, methodName, false);
    }

    Int64 Stopwatch(Action action, String methodName, Boolean writeToConsole) {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        action.Invoke();
        stopwatch.Stop();

        if(writeToConsole)
            Console.WriteLine($"{methodName}: {stopwatch.ElapsedMilliseconds}");

        return stopwatch.ElapsedMilliseconds;
    }
}