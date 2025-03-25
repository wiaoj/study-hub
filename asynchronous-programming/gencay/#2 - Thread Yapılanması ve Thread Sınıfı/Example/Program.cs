#region Thread Sınıfı
//class Program {
//    static void Main() {
//        //Thread thread = new(ThreadMethod); 
//        //Thread thread = new(() => {

//        //});

//        Thread parameterizedThread = new((o) => {
//            for(int i = 0; i < 10; i++) {
//                Console.WriteLine($"Worker Thread {i}");
//            }
//        });

//        parameterizedThread.Start();
//        for(int i = 0; i < 10; i++) {
//            Console.WriteLine($"Main Thread {i}");
//        }
//    }

//    //static void ThreadMethod() {

//    //}
//}

#endregion

#region Thread Id
//Console.WriteLine("Main Thread");
//Console.WriteLine(Environment.CurrentManagedThreadId);
//Console.WriteLine(AppDomain.GetCurrentThreadId());
//Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
//Thread thread1 = new(() => {
//    Console.WriteLine("Worker 1 Thread");
//    Console.WriteLine(Environment.CurrentManagedThreadId);
//    Console.WriteLine(AppDomain.GetCurrentThreadId());
//    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
//});
//thread1.Start();
//Thread thread2 = new(() => {
//    Console.WriteLine("Worker 2 Thread");
//    Console.WriteLine(Environment.CurrentManagedThreadId);
//    Console.WriteLine(AppDomain.GetCurrentThreadId());
//    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
//});
//thread2.Start();
#endregion

#region IsBackground
//int i = 10;
//Thread thread = new(() => {
//	while(i-->=0) {
//		Thread.Sleep(1000);
//    }
//    Console.WriteLine("Worker Thread görevini tamamlandı");
//});

//thread.IsBackground = true;
//thread.Start();
//Console.WriteLine("Main thread görevini tamamladı");
#endregion

#region Thread State
//Int32 i = 10;
//Thread thread = new(() => {
//    while(i-- >= 0) {
//        Thread.Sleep(1000);
//    }
//    Console.WriteLine("Worker Thread görevini tamamlandı");
//});

//thread.Start();
//ThreadState state = thread.ThreadState;

//while(thread.ThreadState != ThreadState.Stopped) {
//    if(state == thread.ThreadState)
//        continue;

//    state = thread.ThreadState;
//    Console.WriteLine(state);
//}

//Console.WriteLine("Main thread görevini tamamladı");
#endregion

#region Locking
//Int32 index = 0;
//Lock @lock = new();
//Thread thread1 = new(() => {
//    @lock.Enter();
//    while(index++ < 10) {
//        Console.WriteLine($"Thread 1: {index}");
//    }
//    @lock.Exit();
//    //lock(@lock) {
//    //    while(index++ < 10) {
//    //        Console.WriteLine($"Thread 1: {index}");
//    //    }
//    //}
//});

//Thread thread2 = new(() => {
//    @lock.Enter();
//    //lock(@lock) {
//        while(index-- >= 1) {
//            Console.WriteLine($"Thread 2: {index}");
//        }
//    //}
//    @lock.Exit();
//});

//thread1.Start();
//thread2.Start();
#endregion

//#region Sleep
//Thread thread = new(() => {
//	for(int i = 1; i <= 10; i++) {
//		Console.WriteLine(i);
//		Thread.Sleep(TimeSpan.FromSeconds(1));
//    }
//});
//thread.Start();
//#endregion

#region Join
//Thread thread1 = new(() => {
//    for(int i = 1; i <= 10; i++) {
//        Console.WriteLine($"Thread 1: {i}");
//        Thread.Sleep(TimeSpan.FromMilliseconds(10));
//    }
//});
//Thread thread2 = new(() => {
//    for(int i = 1; i <= 10; i++) {
//        Console.WriteLine($"Thread 2: {i}");
//        Thread.Sleep(TimeSpan.FromMilliseconds(10));
//    }
//});
//thread1.Start();
//thread1.Join();
//thread2.Start();
#endregion

#region Thread İptal Etme
//bool stopped = false;
//Thread thread = new(() => {
//    while(stopped is false) {
//        Console.Write("...");
//    }
//    Console.WriteLine();
//    Console.WriteLine("Thread tamamlandı");
//});

//thread.Start();
//Thread.Sleep(TimeSpan.FromSeconds(5));
//stopped = true;


//Thread thread = new((cancellationToken) => {
//    var cancelToken = (CancellationTokenSource)cancellationToken;
//    while(cancelToken?.IsCancellationRequested is false) {
//        Console.Write("...");
//    }
//    Console.WriteLine();
//    Console.WriteLine("Thread tamamlandı");
//});

//CancellationTokenSource cancellationTokenSource = new();

//thread.Start(cancellationTokenSource);
//Thread.Sleep(TimeSpan.FromSeconds(5));
//cancellationTokenSource.Cancel();

#endregion

#region Interrupt
Thread thread = new(() => {
	try {
        Console.WriteLine("Uykum geldi..");
        Thread.Sleep(Timeout.Infinite);
    } catch(ThreadInterruptedException) {
        Console.WriteLine("owwhh uyandım...");
	}
});

thread.Start();
Thread.Sleep(TimeSpan.FromSeconds(2));
thread.Interrupt();

#endregion