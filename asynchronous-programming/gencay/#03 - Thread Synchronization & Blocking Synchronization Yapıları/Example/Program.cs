#region Spinning
//Boolean threadCondition = true;

//Thread thread1 = new(() => {
//    while(true) {
//        if(threadCondition is false) continue;
//        for(Int32 i = 1; i <= 10; i++) {
//            Console.WriteLine($"Thread 1: {i}");
//        }
//        threadCondition = false;
//        break;
//    }
//});

//Thread thread2 = new(() => {
//    while(true) {
//        if(threadCondition) continue;
//        for(Int32 i = 10; i > 0; i--) {
//            Console.WriteLine($"Thread 2: {i}");
//        }
//        break;
//    }
//});

//thread1.Start();
//thread2.Start();
#endregion

#region Monitor.Enter ve Monitor.Exit
//Object @lock = new();
//Int32 index = 0;
//Thread thread1 = new(() => { 
//    Monitor.Enter(@lock);
//    try {
//        for(index = 1; index < 10; index++) {
//            Console.WriteLine($"Thread 1: {index}");
//        }
//    } finally {
//        Monitor.Pulse(@lock);
//        Monitor.Exit(@lock);
//    }
//});

//Thread thread2 = new(() => {
//    Monitor.Enter(@lock);
//    try {
//        for(index = 1; index <= 10; index++) {
//            Console.WriteLine($"Thread 2: {index}");
//        }
//    } finally {
//        Monitor.Pulse(@lock);
//        Monitor.Exit(@lock);
//    }
//});

//thread1.Start();
//thread2.Start();
#endregion

#region lockTaken
//Object @lock = new();
//Int32 index = 0;
//Thread thread1 = new(() => {
//    Boolean lockTaken = false;
//    Monitor.Enter(@lock, ref lockTaken);
//    if(lockTaken is false) return;

//    try {
//        for(index = 1; index < 10; index++) {
//            Console.WriteLine($"Thread 1: {index}");
//        }
//    } finally {
//        Monitor.Pulse(@lock);
//        Monitor.Exit(@lock);
//    }
//});

//Thread thread2 = new(() => {
//    Boolean lockTaken = false;
//    Monitor.Enter(@lock, ref lockTaken);
//    if(lockTaken is false) return;

//    try {
//        for(index = 1; index <= 10; index++) {
//            Console.WriteLine($"Thread 2: {index}");
//        }
//    } finally {
//        Monitor.Pulse(@lock);
//        Monitor.Exit(@lock);
//    }
//});

//thread1.Start();
//thread2.Start();
#endregion

#region Monitor.TryEnter
//Object @lock = new();
//Int32 index = 0;
//Thread thread1 = new(() => {
//    Boolean result = Monitor.TryEnter(@lock, TimeSpan.FromMilliseconds(100));
//    if(result is false) return;
//    try {
//        for(index = 1; index < 10; index++) {
//            Console.WriteLine($"Thread 1: {index}");
//        }
//    } finally {
//        Monitor.Pulse(@lock);
//        Monitor.Exit(@lock);
//    }
//});

//Thread thread2 = new(() => {
//    Boolean lockTaken = false; 
//    Monitor.TryEnter(@lock, TimeSpan.FromMilliseconds(1), ref lockTaken);
//    if(lockTaken is false) return;
//    try {
//        for(index = 1; index < 10; index++) {
//            Console.WriteLine($"Thread 2: {index}");
//        }
//    } finally {
//        Monitor.Pulse(@lock);
//        Monitor.Exit(@lock);
//    }
//});

//thread1.Start();
//thread2.Start();
#endregion

#region Mutex
//Mutex mutex = new();
//Int32 index = 0;
//Thread thread1 = new(() => {
//    mutex.WaitOne();
//    for(index = 1; index < 100; index++) {
//        Console.WriteLine($"Thread 1: {index:000}");
//    }
//    mutex.ReleaseMutex();
//});

//Thread thread2 = new(() => {
//    mutex.WaitOne();
//    for(index = 1; index < 1000; index++) {
//        Console.WriteLine($"Thread 2: {index:000}");
//    }
//    mutex.ReleaseMutex();
//});

//thread1.Start();
//thread2.Start();
#endregion

#region Mutex - Single Instance Application
//internal class Program {
//    private static Mutex? mutex;
//    private static readonly String program = "Example";
//    private static void Main(String[] args) {
//        Mutex.TryOpenExisting(program, out mutex);
//        if(mutex is not null) {
//            mutex.Close();
//            return;
//        }

//        mutex = new(true, program);
//        Console.WriteLine("Uygulama Çalışıyor");
//        Console.ReadLine();
//    }
//}

#endregion