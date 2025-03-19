using _2_Data_Sharing_and_Synchronization;

internal class Program {
    private static void Main(String[] args) {
        ReadWriteLock();
    }


    /*
     Mutex ve SpinLock her ikisi de çoklu iş parçacığı (thread) programlamada kullanılan senkronizasyon mekanizmalarıdır. 
    Bunlar, birden fazla iş parçacığının aynı kaynağa (örneğin, bir veri yapısına veya bir dosyaya) aynı anda erişmesini ve 
    böylece olası veri yarışı koşullarını (race conditions) önlemek için kullanılır. Ancak, bu iki mekanizmanın çalışma şekilleri 
    ve kullanım senaryoları farklıdır.

Mutex
    Tanım: Mutex, "Mutual Exclusion Object" (Karşılıklı Dışlama Nesnesi) kelimelerinin kısaltmasıdır. 
     İş parçacıkları arasında karşılıklı dışlamayı sağlayarak, bir kaynağın aynı anda yalnızca bir iş parçacığı 
     tarafından kullanılmasını garanti eder.
    Kullanım: Mutex genellikle farklı iş parçacıkları veya hatta farklı süreçler arasında kaynakların senkronizasyonu için kullanılır.
     İş parçacığı bir Mutex'i "kilitlediğinde" (lock), diğer iş parçacıkları Mutex serbest bırakılana kadar beklemek zorundadır.
    Özellikler: Mutex, işletim sistemi tarafından desteklenir ve iş parçacıkları arası veya süreçler arası senkronizasyon için kullanılabilir. 
     Ayrıca, sahiplik kavramına sahiptir, yani bir Mutex'i kilitleyen iş parçacığı, onu serbest bırakmak zorundadır.
SpinLock
    Tanım: SpinLock, bir kaynağın kilidini almak için aktif olarak döngü yaparak bekleyen bir kilit mekanizmasıdır. 
     Bu, iş parçacığının bloke olmasını önler; bunun yerine, kilidin serbest bırakılmasını beklerken sürekli olarak durumunu kontrol eder.
    Kullanım: SpinLock genellikle çok kısa süreli kilitlemeler için kullanılır çünkü iş parçacığı, kilidi beklerken işlemci zamanını harcar
     (bu "busy-waiting" olarak adlandırılır). Uzun süreli bekleme gerektiren durumlarda uygun değildir.
    Özellikler: SpinLock, işletim sistemi çağrıları yapmadan çalışır ve genellikle kullanıcı modunda gerçekleştirilir. 
     Bu, çok kısa süreli kilitlemeler için etkili olabilir çünkü işletim sistemi seviyesindeki kilitleme ve kilidin 
     açılması işlemleri genellikle daha fazla zaman alır.

Karşılaştırma
    Performans: SpinLock, çok kısa süreli kilitlemeler için idealdir ve bu durumlarda Mutex'ten daha hızlı olabilir. 
    Ancak, uzun süreli bekleme durumlarında Mutex daha verimlidir, çünkü SpinLock işlemci kaynaklarını boşa harcar.
    Kullanım Kolaylığı: Mutex, iş parçacığı veya süreçler arası senkronizasyon için daha kolay ve güvenli bir seçenek olabilir, 
    çünkü işletim sistemi tarafından yönetilir ve deadlock gibi durumları önlemek için ek özelliklere sahiptir.
    Uygulama Alanları: SpinLock, çok düşük gecikme süreleri gerektiren ve kilitleme süresinin çok kısa olduğu senaryolarda tercih edilir. 
    Mutex, daha genel amaçlı senkronizasyon ihtiyaçları için kullanılır, özellikle kaynakların uzun süreli kilitlemeleri gerektiğinde.
     */

    /*
     Hızlı işlemlerde spinlock kullan
     Diğer işlemlerde mutex kullanmak daha mantıklı, işlemci kaynaklarını boşa harcamaya gerek yok
     */

    static void SpinLockBankAccountOperations() {
        /*
         Bu metod, bir banka hesabı üzerinde eş zamanlı para yatırma ve çekme işlemlerini gerçekleştirir. 
         İşlemler SpinLock kullanılarak senkronize edilir
         */
        // 20 adet görev oluşturuluyor: Her biri için 10 para yatırma ve 10 para çekme görevi
        List<Task> tasks = new();
        BankAccount bankAccount = new();
        SpinLock spinLock = new();

        for(Int32 i = 0; i < 10; i++) {
            // Para yatırma görevleri
            tasks.Add(Task.Factory.StartNew(() => {
                for(Int32 j = 0; j < 1000; j++) {
                    Boolean lockTaken = false;
                    try {
                        spinLock.Enter(ref lockTaken);
                        bankAccount.Deposit(100);
                    }
                    finally {
                        if(lockTaken)
                            spinLock.Exit();
                    }
                }
            }));

            // Para çekme görevleri
            tasks.Add(Task.Factory.StartNew(() => {
                for(Int32 j = 0; j < 1000; j++) {
                    Boolean lockTaken = false;
                    try {
                        spinLock.Enter(ref lockTaken);
                        bankAccount.Withdraw(100);
                    }
                    finally {
                        if(lockTaken)
                            spinLock.Exit();
                    }
                }
            }));
        }

        Task.WaitAll(tasks.ToArray());
        Console.WriteLine($"Final balance is {bankAccount.Balance}");
    }

    static void SpinLockRecursionExample() {
        /*
         Bu metod, SpinLock'un özyinelemeli kullanımını gösterir. SpinLock örneği özyinelemeli kilitleme için yapılandırılmıştır.
         */
        SpinLock spinLock = new(true);
        LockRecursion(5, spinLock);

        static void LockRecursion(Int32 x, SpinLock spinLock) {
            Boolean lockTaken = false;
            try {
                spinLock.Enter(ref lockTaken);
            }
            catch(LockRecursionException lockRecursionException) {
                Console.WriteLine("Exception: " + lockRecursionException);
            }
            finally {
                if(lockTaken) {
                    Console.WriteLine($"Took a lock, x = {x}");
                    if(x > 0) {
                        LockRecursion(x - 1, spinLock);
                    }
                    spinLock.Exit();
                }
                else
                    Console.WriteLine($"Failed to take a lock, x = {x}");
            }
        }
    }

    static void MutexBankAccountOperations() {
        /*
         Bu metod, bir banka hesabı üzerinde eş zamanlı para yatırma ve çekme işlemlerini gerçekleştirir. 
         İşlemler Mutex kullanılarak senkronize edilir.
         */
        // 20 adet görev oluşturuluyor: Her biri için 10 para yatırma ve 10 para çekme görevi
        List<Task> tasks = new();
        BankAccount bankAccount = new();
        Mutex mutex = new();

        for(Int32 i = 0; i < 10; i++) {
            // Para yatırma görevleri
            tasks.Add(Task.Factory.StartNew(() => {
                for(Int32 j = 0; j < 1000; j++) {
                    Boolean haveLock = mutex.WaitOne();
                    try {
                        bankAccount.Deposit(100);
                    }
                    finally {
                        if(haveLock)
                            mutex.ReleaseMutex();
                    }
                }
            }));

            // Para çekme görevleri
            tasks.Add(Task.Factory.StartNew(() => {
                for(Int32 j = 0; j < 1000; j++) {
                    Boolean haveLock = mutex.WaitOne();
                    try {
                        bankAccount.Withdraw(100);
                    }
                    finally {
                        if(haveLock)
                            mutex.ReleaseMutex();
                    }
                }
            }));
        }

        Task.WaitAll(tasks.ToArray());
        Console.WriteLine($"Final balance is {bankAccount.Balance}");
    }

    static void MutexMultiAccountOperations() {
        /*
         Bu metod, iki banka hesabı üzerinde eş zamanlı işlemler gerçekleştirir. İşlemler iki Mutex kullanılarak senkronize edilir.
         */
        // Çoklu görevler ve hesaplar için hazırlık
        List<Task> tasks = new();
        BankAccount bankAccount1 = new();
        BankAccount bankAccount2 = new();
        Mutex mutex1 = new();
        Mutex mutex2 = new();

        for(Int32 i = 0; i < 10; i++) {
            // Banka hesabı 1 için para yatırma görevleri
            tasks.Add(Task.Factory.StartNew(() => {
                for(Int32 j = 0; j < 1000; j++) {
                    Boolean haveLock = mutex1.WaitOne();
                    try {
                        bankAccount1.Deposit(1);
                    }
                    finally {
                        if(haveLock)
                            mutex1.ReleaseMutex();
                    }
                }
            }));

            // Banka hesabı 2 için para yatırma görevleri
            tasks.Add(Task.Factory.StartNew(() => {
                for(Int32 j = 0; j < 1000; j++) {
                    Boolean haveLock = mutex2.WaitOne();
                    try {
                        bankAccount2.Deposit(1);
                    }
                    finally {
                        if(haveLock)
                            mutex2.ReleaseMutex();
                    }
                }
            }));

            // İki hesap arasında para transferi görevleri
            tasks.Add(Task.Factory.StartNew(() => {
                for(Int32 k = 0; k < 1000; k++) {
                    Boolean haveLock = WaitHandle.WaitAll(new[] { mutex1, mutex2 });
                    try {
                        bankAccount1.Transfer(bankAccount2, 1);
                    }
                    finally {
                        if(haveLock) {
                            mutex1.ReleaseMutex();
                            mutex2.ReleaseMutex();
                        }
                    }
                }
            }));
        }

        Task.WaitAll(tasks.ToArray());
        Console.WriteLine($"Final balance account1 is {bankAccount1.Balance}");
        Console.WriteLine($"Final balance account2 is {bankAccount2.Balance}");
    }

    static void EnsureSingleInstance() {
        // Uygulama adı sabiti
        const String AppName = "MyApp";
        Mutex mutex;

        try {
            // Mevcut Mutex'i açmaya çalışır. Eğer Mutex zaten varsa, bu uygulamanın bir örneği zaten çalışıyor demektir.
            mutex = Mutex.OpenExisting(AppName);
            Console.WriteLine($"Sorry, {AppName} is already running");
        }
        catch(WaitHandleCannotBeOpenedException) {
            // Mutex mevcut değilse, uygulama çalışabilir. Yeni bir Mutex oluşturulur.
            Console.WriteLine("We can run the program just fine");
            mutex = new Mutex(false, AppName);
        }

        // Kullanıcı bir tuşa basana kadar bekler
        Console.ReadKey(true);

        // Mutex serbest bırakılır
        mutex.ReleaseMutex();
    }

    static void ReadWriteLock() {
        Int32 x = 0;
        List<Task> tasks = [];
        ReaderWriterLockSlim padlock = new();
        /*
        ReaderWriterLockSlim padlock = new(LockRecursionPolicy.SupportsRecursion); --> Tavsiye edilmez
            ReaderWriterLockSlim, özyinelemeli (recursive) kilit kullanımına izin verir. Bu, aynı iş parçacığı içinde,
            zaten alınmış bir kilidi tekrar alabilir ve her alındığında serbest bırakılması gerekir. Örneğin, 
            EnterReadLock() metodunu iki kez çağırmak, ExitReadLock() metodunun da iki kez çağrılmasını gerektirir.
            Bu özellik, karmaşık çağrı zincirlerinde ve özyinelemeli algoritmalarında kullanışlı olabilir.
        */

        for(Int32 i = 0; i < 10; i++) {
            tasks.Add(Task.Factory.StartNew(() => {
               /*
                // Okuma kilidini alır (iki kez - özyinelemeli kullanım)
                //padlock.EnterReadLock();
                //padlock.EnterReadLock();

                Console.WriteLine($"Entered read lock, x = {x}");
                Thread.Sleep(TimeSpan.FromSeconds(5));
                
                // Okuma kilidini serbest bırakır (iki kez - özyinelemeli kullanım)
                //padlock.ExitReadLock();
                //padlock.ExitReadLock();
               */
                padlock.EnterUpgradeableReadLock();
                /*
                    Bu satır, iş parçacığına "yükseltilebilir okuma kilidi" sağlar. 
                    Bu kilitleme modu, iş parçacığına kaynağı okumasına izin verirken,
                    diğer iş parçacıklarının da okuma yapmasına olanak tanır. 
                    Ancak, bu modda olan bir iş parçacığı, gerekirse yazma kilidine geçiş yapabilir. 
                    Bu, okuma işlemi sırasında belirli koşullar altında kaynağın güncellenmesi gerektiğinde kullanışlıdır.
                */
               
                if(i % 2 == 0) {
                    padlock.EnterWriteLock();
                    /*
                        Bu koşul bloğu içinde, iş parçacığı yazma kilidini alır. Bu, i'nin çift olduğu durumlarda gerçekleşir.
                        EnterWriteLock çağrısı yapıldığında, iş parçacığına yazma erişimi verilir ve bu sırada 
                        diğer tüm okuma ve yazma işlemleri bloke olur.
                        Bu, kaynağın güvenli bir şekilde güncellenmesini sağlar, çünkü başka hiçbir iş parçacığı kaynağa erişemez.
                    */

                    x = Random.Shared.Next(10, 1000);
                    /*
                        Kaynağı günceller. Bu örnekte, x değişkeninin değeri rastgele bir sayı ile güncellenir.
                        Bu güncelleme sırasında, diğer iş parçacıklarının kaynağa erişimi engellenmiş olur, böylece veri tutarlılığı korunur.
                    */

                    padlock.ExitWriteLock();
                    /*
                        Yazma kilidini serbest bırakır. Bu işlem, diğer iş parçacıklarının kaynağa tekrar okuma veya yazma yapmasına izin verir.
                        Yazma işlemi tamamlandıktan sonra, bu kilidin serbest bırakılması önemlidir, 
                        aksi takdirde kaynak üzerinde ölümcül kilitleme (deadlock) oluşabilir.
                    */
                }

                Console.WriteLine($"Entered read lock, x = {x}");
                Thread.Sleep(TimeSpan.FromSeconds(5));
                /*
                    İş parçacığı, yükseltilebilir okuma kilidi altında çalışmaya devam eder. 
                    Bu sırada, kaynağın güncel değerini okur ve ekrana yazdırır.
                    Thread.Sleep ile bekleme yapılır, bu sırada diğer iş parçacıkları okuma yapabilir ancak yazma yapamaz.
                */

                padlock.ExitUpgradeableReadLock();
                /*
                    Yükseltilebilir okuma kilidini serbest bırakır. Bu adım, iş parçacığının kaynak üzerindeki işlemlerini tamamladığını 
                    ve diğer iş parçacıklarının artık yazma işlemi yapabileceğini belirtir. Bu kilidi serbest bırakmak, kaynak üzerindeki
                    kilitlemeyi sonlandırır ve diğer iş parçacıklarının kaynağa erişimini sağlar.
                */

                Console.WriteLine($"Exited read lock, x = {x}");
                /*
                    İş parçacığının artık okuma kilidini serbest bıraktığını ve kaynağın güncel değerini ekrana yazdırdığını gösterir.
                    Bu noktada, iş parçacığı kaynak üzerinde herhangi bir kilitleme yapmıyor ve diğer iş parçacıkları kaynağa erişebilir.
                */
            }));
        }

        try { 
            Task.WaitAll([.. tasks]);
        }
        catch(AggregateException aggregateException) { 
            aggregateException.Handle(exception => {
                Console.WriteLine(exception);
                return true;
            });
        }

        while(true) {
            Console.ReadKey();

            // Yazma kilidini alır
            padlock.EnterWriteLock();
            Console.WriteLine("Write lock acquired");

            // Rastgele bir değer atar ve x'i günceller
            Int32 newValue = Random.Shared.Next(10);
            x = newValue;
            Console.WriteLine($"Set x = {x}");

            // Yazma kilidini serbest bırakır
            padlock.ExitWriteLock();
            Console.WriteLine("Write lock released");
        }
    }
}