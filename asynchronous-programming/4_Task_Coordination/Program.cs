


void DemonstrateTaskContinuation() {
    // İlk task başlatılıyor. Bu task su kaynatmayı temsil ediyor.
    Task task = Task.Factory.StartNew(() => {
        Console.WriteLine("Boiling water...");
    });

    // 'ContinueWith' metodu, birinci task tamamlandıktan sonra çalışacak ikinci bir task oluşturur.
    // Bu örnekte, su kaynadıktan sonra fincana dökme işlemi gerçekleştiriliyor.
    Task task2 = task.ContinueWith(previousTask => {
        Console.WriteLine($"Completed task {previousTask.Id}, pour water into cup.");
    });

    // İkinci task'ın tamamlanmasını bekliyor.
    task2.Wait();

    /*
    Web Uygulamasında Dosya Yükleme ve İşleme

    Bir web uygulamasında, kullanıcı tarafından yüklenen bir dosyanın işlenmesi süreci:

    1. Dosya Yükleme: Kullanıcı, web uygulaması aracılığıyla bir dosya yükler. 
    Bu işlem asenkron olarak gerçekleştirilir. Dosya yükleme işlemi, sunucu tarafında bir 
    ağ kaynağına veya dosya sisteminde bir konuma dosya kaydetmeyi içerebilir.

    2. Dosya İşleme: Dosya başarıyla yüklendikten sonra, bu dosyanın içeriği işlenmelidir. 
    Bu, dosyanın analiz edilmesi, veritabanına kaydedilmesi veya başka bir işleme tabi tutulması olabilir.

    'ContinueWith' metodu, dosya yükleme işlemi tamamlandıktan sonra dosya işleme işlemini tetiklemek için kullanılır.
    Böylece, dosya yükleme işlemi başarıyla tamamlandığında, otomatik olarak dosya işleme işlemi başlatılır. 
    Bu yaklaşım, işlemlerin sıralı ve bağımlı bir şekilde yürütülmesini sağlar ve kodun okunabilirliğini artırır.

    Örnek Kullanım:
    - Dosya yükleme task'ı başlatılır.
    - 'ContinueWith' ile dosya yükleme task'ının tamamlanmasını takiben dosya işleme task'ı başlatılır.
    - Her iki işlem de asenkron olarak gerçekleştirilir ve birbirlerine bağlıdır.
    */
}

void DemonstrateTaskContinuationWithAllAndAny() {
    // İki asenkron task oluşturuluyor.
    Task<String> task1 = Task.Factory.StartNew(() => "Task 1");
    Task<String> task2 = Task.Factory.StartNew(() => "Task 2");

    // 'ContinueWhenAll' metodu, tüm task'lar tamamlandığında çalışacak bir devam task'ı oluşturur.
    // Bu örnekte, tüm task'ların sonuçlarını yazdırmak için kullanılır.
    // Yorum satırları içindeki kod, her iki task da tamamlandığında çalışacak.
    /*
    Task task3 = Task.Factory.ContinueWhenAll(new[] {task1, task2}, tasks => {
        Console.WriteLine("Tasks completed:");
        foreach(Task<String> task in tasks) {
            Console.WriteLine(" - " + task.Result);
        }
        Console.WriteLine("All tasks done.");
    });
    */

    // 'ContinueWhenAny' metodu, herhangi bir task tamamlandığında çalışacak bir devam task'ı oluşturur.
    // Bu örnekte, tamamlanan ilk task'ın sonucunu yazdırmak için kullanılır.
    Task task3 = Task.Factory.ContinueWhenAny(new[] { task1, task2 }, task => {
        Console.WriteLine("A task completed:");
        Console.WriteLine(" - " + task.Result);
        Console.WriteLine("Continuing with other tasks...");
    });

    // Devam task'ının tamamlanmasını bekliyor.
    task3.Wait();
}

void DemonstrateNestedTasksAndContinuations() {
    /*
    TaskCreationOptions

    - None: Varsayılan seçenek, özel bir davranış belirtmez.
    - PreferFairness: Görev planlayıcısının, görevleri daha adil bir sırayla çalıştırmasını tercih eder.
    - LongRunning: Görevin uzun süre çalışacağını belirtir, bu durumda farklı bir thread yönetimi stratejisi kullanılabilir.
    - AttachedToParent: Oluşturulan görevin, üst göreve bağlı olarak çalışmasını sağlar. Üst görev tamamlanmadan alt görevlerin 
     tamamlanmasını bekler.
    - DenyChildAttach: Alt görevlerin bu göreve bağlanmasını engeller.
    - HideScheduler: Görevin, varsayılan görev planlayıcısını kullanmasını sağlar, özel bir planlayıcı kullanılmaz.
    - RunContinuationsAsynchronously: Görev tamamlandığında, devam görevlerinin asenkron olarak çalıştırılmasını sağlar.

    TaskContinuationOptions

    - None: Varsayılan seçenek, özel bir davranış belirtmez.
    - PreferFairness: Görev planlayıcısının, devam görevlerini daha adil bir sırayla çalıştırmasını tercih eder.
    - LongRunning: Devam görevinin uzun süre çalışacağını belirtir.
    - AttachedToParent: Devam görevinin, üst göreve bağlı olarak çalışmasını sağlar.
    - DenyChildAttach: Alt görevlerin bu devam görevine bağlanmasını engeller.
    - HideScheduler: Devam görevinin, varsayılan görev planlayıcısını kullanmasını sağlar.
    - LazyCancellation: İptal talebi geldiğinde, görevin hemen iptal edilmemesini sağlar.
    - RunContinuationsAsynchronously: Görev tamamlandığında, devam görevlerinin asenkron olarak çalıştırılmasını sağlar.
    - NotOnRanToCompletion: Görev başarıyla tamamlandığında devam görevinin çalışmamasını sağlar.
    - NotOnFaulted: Görev hata ile sonuçlandığında devam görevinin çalışmamasını sağlar.
    - NotOnCanceled: Görev iptal edildiğinde devam görevinin çalışmamasını sağlar.
    - OnlyOnRanToCompletion: Yalnızca görev başarıyla tamamlandığında devam görevinin çalışmasını sağlar.
    - OnlyOnFaulted: Yalnızca görev hata ile sonuçlandığında devam görevinin çalışmasını sağlar.
    - OnlyOnCanceled: Yalnızca görev iptal edildiğinde devam görevinin çalışmasını sağlar.

    Bu seçenekler, Task'ların ve devam işlemlerinin davranışlarını daha iyi kontrol etmek için kullanılır ve belirli senaryolara 
     göre optimize edilmiş iş akışları oluşturulmasına olanak tanır.
*/

    // Ana task oluşturuluyor.
    Task parent = new(() => {
        // İç içe bir child task oluşturuluyor ve AttachedToParent seçeneği ile ana task'a bağlanıyor.
        Task child = new(() => {
            Console.WriteLine("Child task starting.");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            Console.WriteLine("Child task finishing.");
            throw new Exception(); // Child task bir hata fırlatıyor.
        }, TaskCreationOptions.AttachedToParent);

        // Child task başarıyla tamamlandığında çalışacak devam task'ı.
        Task completionHandler = child.ContinueWith(task => {
            Console.WriteLine($"Hooray, task {task.Id}'s state is {task.Status}");
        }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

        // Child task hata ile sonuçlandığında çalışacak devam task'ı.
        Task failHandler = child.ContinueWith(task => {
            Console.WriteLine($"Oops, task {task.Id}'s state is {task.Status}");
        }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

        child.Start();
    });

    parent.Start();

    try {
        parent.Wait();
    }
    catch(AggregateException aggregateException) {
        aggregateException.Handle(exception => true);
    }
}

void DemonstrateBarrierForTaskSynchronization() {
    // Barrier nesnesi oluşturuluyor. İki katılımcı (participant) için ayarlanmış.
    // Her bir fazın sonunda çağrılacak bir eylem tanımlanıyor.
    Barrier barrier = new(2, barrier => {
        Console.WriteLine($"Phase {barrier.CurrentPhaseNumber} is finished");
        // barrier.ParticipantsRemaining: Kalan katılımcı sayısını verir.
        // barrier.AddParticipants(): Daha fazla katılımcı eklemek için kullanılır.
    });

    // Su kaynatma işlemini temsil eden görev.
    void Water() {
        Console.WriteLine("Putting the kettle on (takes a bit longer)");
        Thread.Sleep(TimeSpan.FromSeconds(2));
        barrier.SignalAndWait(); // Su kaynatma işlemi tamamlandı, diğer görevi bekliyor.
        Console.WriteLine("Pouring water into cup.");
        barrier.SignalAndWait(); // Su dökme işlemi tamamlandı, diğer görevi bekliyor.
        Console.WriteLine("Putting the kettle away");
    }

    // Çay fincanı hazırlama işlemini temsil eden görev.
    void Cup() {
        Console.WriteLine("Finding the nicest cup of tea (fast)");
        barrier.SignalAndWait(); // Fincan bulma işlemi tamamlandı, diğer görevi bekliyor.
        Console.WriteLine("Adding tea.");
        barrier.SignalAndWait(); // Çay ekleme işlemi tamamlandı, diğer görevi bekliyor.
        Console.WriteLine("Adding sugar");
    }

    // Görevler başlatılıyor.
    Task water = Task.Factory.StartNew(Water);
    Task cup = Task.Factory.StartNew(Cup);

    // İki görev de tamamlandığında çalışacak olan devam görevi.
    Task tea = Task.Factory.ContinueWhenAll(new[] { water, cup }, tasks => {
        Console.WriteLine("Enjoy your cup of tea.");
    });

    // Çay hazırlama işleminin tamamlanmasını bekliyoruz.
    tea.Wait();
}

void DemonstrateCountdownEventForTaskCompletion() {
    /*
        Başlangıç Sayacı: CountdownEvent oluşturulurken bir başlangıç sayacı değeri verilir. 
        Bu değer, CountdownEvent'in sıfıra düşmesi gereken olay sayısını temsil eder.
        Signal Metodu: Her Signal çağrısı, CountdownEvent'in sayacını bir azaltır. 
        Sayac sıfıra düştüğünde, bekleyen tüm görevler devam edebilir.
        Wait Metodu: CountdownEvent'in sayacı sıfıra düşene kadar çağrı yapan görevin beklemesini sağlar.
     */
    // Toplam görev sayısı belirleniyor.
    Int32 taskCount = 5;
    // CountdownEvent oluşturuluyor ve başlangıç sayacı taskCount olarak ayarlanıyor.
    CountdownEvent countdownEvent = new(taskCount);

    // Belirlenen sayıdan daha fazla görev başlatılıyor.
    for(Int32 i = 0; i < taskCount; i++) {
        Task.Factory.StartNew(() => {
            Console.WriteLine($"Entering task {Task.CurrentId}");
            // Rastgele bir süre bekleniyor.
            Thread.Sleep(TimeSpan.FromMilliseconds(Random.Shared.Next(3000)));
            // CountdownEvent'in sayacı bir azaltılıyor.
            countdownEvent.Signal();
            Console.WriteLine($"Exiting task {Task.CurrentId}");
        });
    }

    // Tüm görevlerin tamamlanmasını bekleyen final görevi.
    Task finalTask = Task.Factory.StartNew(() => {
        Console.WriteLine($"Waiting for other tasks to complete in {Task.CurrentId}");
        // CountdownEvent'in sayacı sıfıra düşene kadar bekleniyor.
        countdownEvent.Wait();
        Console.WriteLine("All tasks completed");
    });

    // Final görevinin tamamlanmasını bekliyoruz.
    finalTask.Wait();
}

void DemonstrateAutoResetEventForTaskSynchronization() {
    // AutoResetEvent nesnesi oluşturuluyor. Başlangıç durumu olarak 'false' (sinyal yok) belirleniyor.
    AutoResetEvent @event = new(false);

    // Su kaynatma görevi başlatılıyor.
    Task.Factory.StartNew(() => {
        Console.WriteLine("Boiling water");
        @event.Set(); // Su kaynadıktan sonra sinyal gönderiliyor.
    });

    // Çay yapma görevi başlatılıyor.
    Task makeTea = Task.Factory.StartNew(() => {
        Console.WriteLine("Waiting for water...");
        @event.WaitOne(); // Su kaynamasını bekliyor.
        Console.WriteLine("Here is your tea");

        // AutoResetEvent sinyali otomatik olarak sıfırlanır, bu yüzden burada 'false' döner.
        Boolean ok = @event.WaitOne(1000); // 1 saniye bekleyip tekrar kontrol ediyor.
        if(ok) {
            Console.WriteLine("Enjoy your tea");
        }
        else {
            Console.WriteLine("No tea for you");
        }
    });

    // Çay yapma görevinin tamamlanmasını bekliyoruz.
    makeTea.Wait();

    /*
        ManualResetEventSlim @event = new();
        Task.Factory.StartNew(() => {
            Console.WriteLine("Boiling water");
            @event.Set();
        });

        Task makeTea = Task.Factory.StartNew(() => {
            Console.WriteLine("Waiting for water...");
            @event.Wait(); // ManualResetEventSlim sinyalini bekliyor.
            Console.WriteLine("Here is your tea");

            Boolean ok = @event.Wait(1000); // ManualResetEventSlim sinyalini tekrar bekliyor.
            if(ok) {
                Console.WriteLine("Enjoy your tea");
            }
            else {
                Console.WriteLine("No tea for you");
            }
        });

        makeTea.Wait();

        ManualResetEventSlim, Set metodu çağrıldığında sinyalli duruma geçer ve Reset çağrılmadıkça sinyalli durumda kalır.
        Bu, birden fazla görevin aynı sinyali beklediği durumlarda kullanışlıdır.
        Örnekte, su kaynatma görevi tamamlandığında çay yapma görevine sinyal gönderiliyor ve bu sinyal elle sıfırlanana kadar aktif kalır.
    */
}

void DemonstrateSemaphoreSlimForLimitedResourceAccess() {
    /*
     SemaphoreSlim sınıfı, belirli sayıda kaynağa veya işlem kapasitesine sınırlı erişim sağlamak için kullanılır. Bu sınıf, birden 
     fazla görevin aynı kaynaklara erişimini sınırlamak ve yönetmek için kullanışlıdır.
    
    SemaphoreSlim'in temel özellikleri

     Başlangıç ve Maksimum Kaynak Sayısı: Semaphore oluşturulurken başlangıçta kaç kaynağa izin verileceği ve maksimum kaç 
     kaynağa kadar izin verilebileceği belirlenir.
     Wait ve WaitAsync Metotları: Bir görev, semaphore'dan kaynak almak için bu metotları kullanır. 
     Eğer kaynak mevcut değilse, görev beklemeye alınır.
     Release Metodu: Bir görev, işini tamamladığında semaphore'a kaynak iade eder. Bu, diğer bekleyen görevlerin devam etmesini sağlar.
     CurrentCount Özelliği: Semaphore'da şu anda ne kadar kaynak olduğunu gösterir.
     Bu yapı, özellikle kaynakların sınırlı olduğu ve bu kaynaklara eş zamanlı erişimin kontrol altında tutulması 
     gereken durumlarda kullanılır. Örneğimizde, aynı anda en fazla iki görevin işlem yapmasına izin veriliyor ve 
     kullanıcı girişiyle semaphore'a ek kaynaklar ekleniyor.
     */
    // SemaphoreSlim nesnesi oluşturuluyor. Başlangıçta 2, maksimum 10 kaynağa izin veriliyor.
    SemaphoreSlim semaphore = new(2, 10);

    // 20 görev başlatılıyor.
    for(int i = 0; i < 20; i++) {
        Task.Factory.StartNew(() => {
            Console.WriteLine($"Entering task {Task.CurrentId}");
            semaphore.Wait(); // Semaphore'dan bir kaynak alınıyor (ReleaseCount azalıyor).
            Console.WriteLine($"Processing task {Task.CurrentId}");
            // Burada işlem yapılır, örneğin veri işleme veya dosya erişimi.
            // Görev tamamlandığında, semaphore.Release() çağrılmalıdır (bu örnekte çağrılmamış).
        });
    }

    // Semaphore'un mevcut kaynak sayısını kontrol ediyoruz.
    while(semaphore.CurrentCount <= 2) {
        Console.WriteLine($"Semaphore count: {semaphore.CurrentCount}");
        Console.ReadKey();
        semaphore.Release(2); // Semaphore'a 2 kaynak daha ekleniyor.
    }
}