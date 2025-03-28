#region Semaphore
//List<Int32> numbers = [];
//Semaphore semaphore = new(3, 3);
//Thread thread1 = new(() => {
//    semaphore.WaitOne();
//    for(Int32 i = 1; i < 10; i++) {
//        Console.WriteLine($"Thread-1: {i}");
//        numbers.Add(i);
//        Thread.Sleep(1000);
//    }
//    semaphore.Release();
//});

//Thread thread2 = new(() => {
//    semaphore.WaitOne();
//    for(Int32 i = 10; i < 20; i++) {
//        Console.WriteLine($"Thread-2: {i}");
//        numbers.Add(i);
//        Thread.Sleep(1500);
//    }
//    semaphore.Release();
//}); 

//Thread thread3 = new(() => {
//    semaphore.WaitOne();
//    for(Int32 i = 20; i <= 30; i++) {
//        Console.WriteLine($"Thread-3: {i}");
//        numbers.Add(i);
//        Thread.Sleep(2000);
//    }
//    semaphore.Release();
//});
//thread1.Start();
//thread2.Start();
//thread3.Start();
#endregion

#region SemaphoreSlim
List<Int32> numbers = [];
using SemaphoreSlim semaphoreSlim = new(2, 2);
Thread thread1 = new(() => {
    semaphoreSlim.Wait();
    for(Int32 i = 1; i < 10; i++) {
        Console.WriteLine($"Thread-1: {i}");
        numbers.Add(i);
        Thread.Sleep(100);
    }
    semaphoreSlim.Release();
});

Thread thread2 = new(() => {
    semaphoreSlim.Wait();
    for(Int32 i = 10; i < 20; i++) {
        Console.WriteLine($"Thread-2: {i}");
        numbers.Add(i);
        Thread.Sleep(150);
    }
    semaphoreSlim.Release();
});

Thread thread3 = new(() => {
    semaphoreSlim.Wait(100);
    for(Int32 i = 20; i <= 30; i++) {
        Console.WriteLine($"Thread-3: {i}");
        numbers.Add(i);
        Thread.Sleep(200);
    }
    semaphoreSlim.Release();
});
thread1.Start();
thread2.Start();
thread3.Start();
#endregion