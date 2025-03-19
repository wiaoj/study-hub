

Console.WriteLine("Main program done.");
Console.ReadKey();

// Test1: Eş zamanlı olarak farklı görevler başlatır ve hemen çıktı üretir.
static void RunConcurrentTasksWithImmediateOutput() {
    Action<Char> Write = (character) => {
        Int32 i = 1000;
        while(i-- > 0)
            Console.Write(character);
    };
    
    Task.Factory.StartNew(() => Write('.')); // Yeni bir görev başlatır ve '.' karakterini yazar.

    Task task = new(() => Write('?')); // '?' karakterini yazacak bir görev oluşturur.

    task.Start(); // '?' yazacak görevi başlatır.

    Write('-'); // Ana akışta '-' karakterini yazar.
}

// Test2: Farklı parametrelerle görevler başlatır.
static void StartTasksWithDifferentParameters() {
    Action<Object> Write = (@object) => { 
        Int32 i = 1000;
        while(i-- > 0)
            Console.Write(@object);
    };

    Task task = new(Write, "hello"); // "hello" mesajını yazacak bir görev oluşturur.

    task.Start(); // "hello" yazacak görevi başlatır.

    Task.Factory.StartNew(Write, 123); // 123 sayısını yazacak yeni bir görev başlatır.
}

// Test3: Görevler içinde string uzunluklarını hesaplar ve sonuçları ekrana yazdırır.
static void CalculateStringLengthInTasks() {
    Func<Object, Int32> TextLenght = (@object) => {
        Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {@object}...");
        return $"{@object}".Length;
    };

    String text1 = "testing", text2 = "this";

    Task<Int32> task1 = new(TextLenght, text1); // 'text1'in uzunluğunu hesaplayacak görev.
    task1.Start();

    Task<Int32> task2 = Task.Factory.StartNew<Int32>(TextLenght, text2); // 'text2'nin uzunluğunu hesaplayacak görev.

    // Görevlerin sonuçlarını yazdırır.
    Console.WriteLine($"Length of {text1} is {task1.Result}");
    Console.WriteLine($"Length of {text2} is {task2.Result}");
}

// Test4: Görev iptalini ve iptal geri çağırmalarını gösterir.
static void DemonstrateTaskCancellationWithCallbacks() {
    CancellationTokenSource cancellationTokenSource = new();
    CancellationToken token = cancellationTokenSource.Token;

    // İptal talebi geldiğinde mesaj yazacak bir geri çağırma kaydeder.
    token.Register(() => {
        Console.WriteLine("Cancellation has been requested.");
    });

    // Sonsuz döngü içeren ve iptal talebi geldiğinde iptal edilen bir görev oluşturur.
    Task task = new(() => {
        Int32 i = 0;
        while(true) {
            // Yorum satırları alternatif iptal kontrol yöntemlerini gösterir:
            // if(token.IsCancellationRequested)
            //     break;
            // if(token.IsCancellationRequested)
            //     throw new OperationCanceledException();

            token.ThrowIfCancellationRequested(); // İptal talebi varsa hata fırlatır.

            Console.WriteLine($"{i++}");
        }
    }, token);

    task.Start();

    // İptal talebi geldiğinde mesaj yazacak başka bir görev başlatır.
    Task.Factory.StartNew(() => {
        token.WaitHandle.WaitOne();
        Console.WriteLine("Wait handle released, cancelation was requested.");
    });

    Console.ReadKey();
    cancellationTokenSource.Cancel(); // İptal talebini gönderir.
}

// Test5: Bağlantılı iptal tokenları kullanır.
static void UseLinkedCancellationTokens() {
    CancellationTokenSource planned = new(),
                            preventative = new(),
                            emergency = new();

    CancellationToken[] tokens = [planned.Token, preventative.Token, emergency.Token];

    // Üç iptal tokenını birleştiren bir CancellationTokenSource oluşturur.
    CancellationTokenSource paranoid = CancellationTokenSource.CreateLinkedTokenSource(tokens);

    CancellationToken paranoidToken = paranoid.Token;

    // İptal talebi geldiğinde iptal edilecek bir görev başlatır.
    Task.Factory.StartNew(() => {
        Int32 i = 0;
        while(true) {
            paranoidToken.ThrowIfCancellationRequested();
            Console.WriteLine($"{i++}");
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }, paranoidToken);

    Console.ReadKey();
    emergency.Cancel(); // Acil durum iptalini tetikler.
}

// Test6: Zaman aşımı ile iptal işlemi uygular.
static void ImplementCancellationWithTimeout() {
    CancellationTokenSource cancellationTokenSource = new();
    CancellationToken token = cancellationTokenSource.Token;
    Task task = new(() => {
        // Yorum satırları alternatif bekleme yöntemlerini gösterir:
        // Thread.SpinWait(1);
        // SpinWait.SpinUntil();
        /*
         Thread.SpinWait(1);: Bu yöntem, iş parçacığını çok kısa bir süre için aktif bir şekilde bekletir (döndürür). 
          1 parametresi, döngü sayısını belirtir. Bu yöntem genellikle çok kısa süreli beklemeler için kullanılır ve iş parçacığı, 
          bu süre zarfında başka işlere geçiş yapmaz.

         SpinWait.SpinUntil(() => someCondition);: Bu yöntem, belirtilen bir koşul sağlanana kadar 
          iş parçacığını aktif bir şekilde bekletir. Örneğin, SpinWait.SpinUntil(() => someFlag == true); 
          şeklinde kullanıldığında, someFlag değişkeni true değerine eşit olana kadar iş parçacığı bekletilir. 
          Bu yöntem, belirli bir koşulun gerçekleşmesini beklerken kullanılır ve iş parçacığı, bu süre zarfında başka işlere geçiş yapmaz.
         */

        Console.WriteLine("Press any key to disarm; you have 5 seconds");
        Boolean cancelled = token.WaitHandle.WaitOne(TimeSpan.FromSeconds(5));
        Console.WriteLine(cancelled ? "Bomb disarmed." : "BOOMM!!!");
    }, token);
    task.Start();

    Console.ReadKey();
    cancellationTokenSource.Cancel(); // İptal talebini gönderir.
}

// Test7: Zaman aşımı ile görev iptalini ele alır.
static void HandleTaskCancellationWithTimeout() {
    CancellationTokenSource cancellationTokenSource = new();
    CancellationToken token = cancellationTokenSource.Token;

    Task task1 = new(() => {
        Console.WriteLine("I take 5 seconds");

        for(Int32 i = 0; i < 5; i++) {
            token.ThrowIfCancellationRequested();
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        Console.WriteLine("I'm done.");
    }, token);

    task1.Start();

    Task task2 = Task.Factory.StartNew(() => Thread.Sleep(TimeSpan.FromSeconds(3)), token);

    // Yorum satırları alternatif bekleme ve iptal kontrol yöntemlerini gösterir:
    // Task.WaitAll(task1, task2);
    // Task.WaitAny(task1, task2);
    // Console.ReadKey();
    // cancellationTokenSource.Cancel();

    // Belirli bir süre içinde her iki görevi de bekler ve sonra iptal talebini kontrol eder.
    Task.WaitAll([task1, task2], 4000, token);

    Console.WriteLine($"Task task1 status is {task1.Status}");
    Console.WriteLine($"Task task2 status is {task2.Status}");
}

// Test8: Birden fazla görevdeki hataları ele alır.
static void HandleExceptionsInMultipleTasks() {
    try {
        Test8Local();
    }
    catch(AggregateException aggregateException) {
        foreach(Exception exception in aggregateException.InnerExceptions) {
            Console.WriteLine($"Handled elsewhere: {exception.GetType()} from {exception.Source}");
        }
    }

    static void Test8Local() {
        Task task1 = Task.Factory.StartNew(() => {
            throw new InvalidOperationException("Can't do this!") {
                Source = nameof(task1),
            };
        });

        Task task2 = Task.Factory.StartNew(() => {
            throw new AccessViolationException("Can't access this!") {
                Source = nameof(task2),
            };
        });

        try {
            Task.WaitAll(task1, task2);
        }
        catch(AggregateException aggregateException) {
            // Yorum satırları alternatif hata işleme yöntemlerini gösterir:
            // foreach(Exception exception in aggregateException.InnerExceptions) {
            //     Console.WriteLine($"Exception {exception.GetType()} from {exception.Source}");
            // }
            aggregateException.Handle(exception => {
                if(exception is InvalidOperationException) {
                    Console.WriteLine("Invalid operation");
                    return true;
                }

                return false;
            });
        }
    }
}