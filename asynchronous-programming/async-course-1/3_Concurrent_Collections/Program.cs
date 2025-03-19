using System.Collections.Concurrent;
/*  eşzamanlılık (concurrency) */
/*
    "Thread-safe" terimi, çoklu iş parçacığı (thread) ortamlarında bir programın veya kod bölümünün güvenli bir şekilde çalışabilmesini 
     ifade eder. Bir kod parçasının veya veri yapısının thread-safe olduğunu söylemek, birden fazla thread'in aynı anda bu kodu 
     çalıştırdığında veya bu veri yapısına eriştiğinde herhangi bir hata, veri bozulması veya beklenmeyen davranış olmadığı anlamına gelir.

    Thread-safe olmanın önemi, özellikle çok iş parçacıklı uygulamalarda ortaya çıkar. Çoklu thread'ler aynı kaynaklara 
     (örneğin, değişkenlere, dosyalara, veritabanlarına) eriştiğinde, eğer bu erişimler düzgün bir şekilde yönetilmezse, 
     çeşitli sorunlar ortaya çıkabilir:

    Yarış Koşulları (Race Conditions): İki veya daha fazla thread'in aynı veriye eş zamanlı erişimi sonucu, verinin beklenmedik 
     bir şekilde değişmesi durumudur. Bu, veri bütünlüğünün bozulmasına yol açabilir.

    Ölü Kilitler (Deadlocks): Birden fazla thread'in birbirini sonsuza dek beklemesi ve böylece programın ilerlemesinin durması durumudur.

    Açlık (Starvation): Bazı thread'lerin diğerlerine göre daha az kaynak alması veya hiç kaynak alamaması durumudur.

    Yanlış Paylaşım (False Sharing): Birden fazla thread'in, performansı düşürebilecek şekilde, aynı bellek alanını gereksiz 
     yere paylaşması durumudur.

    Thread-safe olmayan bir kodu thread-safe hale getirmek için çeşitli yöntemler kullanılır:

    Kilit Mekanizmaları (Locks): Kritik bölümlere aynı anda sadece bir thread'in erişmesini sağlamak için kilitler kullanılır.

    Atomik İşlemler: Bazı işlemler, kesintiye uğramadan tamamlanacak şekilde tasarlanmıştır ve bu işlemler thread-safe olarak kabul edilir.

    Thread-Safe Koleksiyonlar: ConcurrentDictionary, ConcurrentQueue, ConcurrentStack gibi .NET'teki thread-safe koleksiyonlar, 
     çoklu thread ortamlarında güvenli bir şekilde kullanılabilir.

    İzolasyon: Verilerin kopyalarını oluşturarak her thread'in kendi kopyası üzerinde çalışmasını sağlamak.

    Thread-safe programlama, özellikle sunucu uygulamaları, paralel hesaplama ve her türlü çoklu iş parçacıklı 
     sistemlerde kritik öneme sahiptir. 
 */

void DemonstrateConcurrentDictionaryUsage() {
    // ConcurrentDictionary oluşturuluyor. Bu, thread-safe bir koleksiyondur ve
    // eşzamanlı erişimde veri bütünlüğünü korur.
    ConcurrentDictionary<String, String> capitals = new();

    // Paris'i ekleme işlemini gerçekleştiren yerel fonksiyon.
    void AddParis() {
        // Paris'i eklemeye çalışıyor ve başarı durumunu kontrol ediyor.
        // Eşzamanlılık durumunda, birden fazla thread bu metodu aynı anda çağırabilir,
        // ancak 'TryAdd' sayesinde herhangi bir çakışma olmaz.
        Boolean success = capitals.TryAdd("France", "Paris");
        String who = Task.CurrentId.HasValue ? $"Task {Task.CurrentId}" : "Main Thread";
        Console.WriteLine($"{who} {(success ? "added" : "did not add")} the element.");
    }

    // AddParis fonksiyonunu yeni bir task üzerinde çalıştırıyor ve bekliyor.
    // Bu, eşzamanlılık senaryosunu simüle eder.
    Task.Factory.StartNew(AddParis).Wait();
    // AddParis fonksiyonunu ana thread üzerinde çalıştırıyor.
    // Bu, eşzamanlı erişim durumunu test eder.
    AddParis();

    // Rusya için başkenti Leningrad olarak ayarlıyor.
    capitals["Russia"] = "Leningrad";
    // Aşağıdaki satır, Rusya'nın başkentini 'Moscow' olarak günceller.
    // capitals["Russia"] = "Moscow";
    // Console.WriteLine(capitals["Russia"]); // Bu, 'Moscow' yazdırır.

    // Rusya için başkenti güncelliyor ve eski değeri yeni değerle birleştiriyor.
    // 'AddOrUpdate' metodu, eşzamanlılık durumunda güvenli bir şekilde güncelleme yapar.
    capitals.AddOrUpdate("Russia", "Moscow", (k, old) => old + " --> Moscow");
    Console.WriteLine($"The capial of Russia is {capitals["Russia"]}");

    // İsveç için başkenti Uppsala olarak ayarlıyor.
    capitals["Sweden"] = "Uppsala";
    // İsveç için başkenti getiriyor veya ekliyor (burada getiriyor çünkü zaten eklenmiş).
    // 'GetOrAdd' metodu, eşzamanlılık durumunda güvenli bir şekilde veri alır veya ekler.
    String capitalOfSweden = capitals.GetOrAdd("Sweden", "Stockholm");
    Console.WriteLine($"The capital of Sweden is {capitalOfSweden}");

    // Rusya'yı silmeye çalışıyor ve sonucu kontrol ediyor.
    // 'TryRemove' metodu, eşzamanlılık durumunda güvenli bir şekilde silme işlemi yapar.
    const String toRemove = "Russia";
    Boolean didRemove = capitals.TryRemove(toRemove, out String? removed);
    if(didRemove) {
        Console.WriteLine($"We just removed {removed}");
    }
    else {
        Console.WriteLine($"Failed to remove the capital of {toRemove}");
    }

    // Tüm başkentleri listeliyor.
    // Bu döngü, eşzamanlılık durumunda da güvenli bir şekilde çalışır.
    foreach(KeyValuePair<String, String> capital in capitals) {
        Console.WriteLine($" - {capital.Value} is the capital of {capital.Key}");
    }
}

void DemonstrateConcurrentQueueUsage() {
    // ConcurrentQueue oluşturuluyor. Bu, thread-safe bir FIFO (ilk giren ilk çıkar) kuyruktur.
    ConcurrentQueue<Int32> queue = new();
    // Kuyruğa elemanlar ekleniyor. Bu işlemler eşzamanlılık durumunda güvenlidir.
    queue.Enqueue(1); // 1 kuyruğa ekleniyor.
    queue.Enqueue(2); // 2 kuyruğa ekleniyor.

    // Şu anda kuyruk durumu: 2 1 <- front

    // Kuyruktan bir eleman çıkarmayı deniyor ve başarılı olursa yazdırıyor.
    // 'TryDequeue' metodu, eşzamanlılık durumunda güvenli bir şekilde eleman çıkarır.
    if(queue.TryDequeue(out Int32 result)) {
        Console.WriteLine($"Removed element {result}"); // 1 çıkarılır ve yazdırılır.
    }

    // Kuyruğun önündeki elemanı görmeyi deniyor ve başarılı olursa yazdırıyor.
    // 'TryPeek' metodu, eşzamanlılık durumunda güvenli bir şekilde kuyruğun önündeki elemanı gösterir.
    if(queue.TryPeek(out result)) {
        Console.WriteLine($"Front element is {result}"); // Şu anda ön eleman 2'dir.
    }
}

void DemonstrateConcurrentStackUsage() {
    // ConcurrentStack oluşturuluyor. Bu, thread-safe bir LIFO (son giren ilk çıkar) yığındır.
    ConcurrentStack<Int32> stack = new();

    // Yığına elemanlar ekleniyor. Bu işlemler eşzamanlılık durumunda güvenlidir.
    stack.Push(1); // 1 yığına ekleniyor.
    stack.Push(2); // 2 yığına ekleniyor.
    stack.Push(3); // 3 yığına ekleniyor.
    stack.Push(4); // 4 yığına ekleniyor.

    // Yığının en üstündeki elemanı görmeyi deniyor ve başarılı olursa yazdırıyor.
    // 'TryPeek' metodu, eşzamanlılık durumunda güvenli bir şekilde yığının en üstündeki elemanı gösterir.
    if(stack.TryPeek(out Int32 result)) {
        Console.WriteLine($"{result} is on top"); // En üstteki eleman (4) yazdırılır.
    }

    // Yığından bir eleman çıkarmayı deniyor ve başarılı olursa yazdırıyor.
    // 'TryPop' metodu, eşzamanlılık durumunda güvenli bir şekilde yığından eleman çıkarır.
    if(stack.TryPop(out result)) {
        Console.WriteLine($"Popped {result}"); // En üstteki eleman (4) çıkarılır ve yazdırılır.
    }

    // Yığından birden fazla eleman çıkarmayı deniyor ve başarılı olursa yazdırıyor.
    // 'TryPopRange' metodu, eşzamanlılık durumunda güvenli bir şekilde yığından birden fazla eleman çıkarır.
    Int32[] items = new Int32[5];
    if(stack.TryPopRange(items, 0, 5) > 0) {
        // Çıkarılan elemanları bir string'e dönüştürüyor ve yazdırıyor.
        String text = String.Join(", ", items.Select(item => item.ToString()));
        Console.WriteLine($"Popped these items: {text}"); // Çıkarılan elemanlar yazdırılır.
    }

    /*
        4 is on top
        Popped 4
        Popped these items: 3, 2, 1, 0, 0
     */
}

void DemonstrateConcurrentBagUsage() {
    // ConcurrentBag oluşturuluyor. Bu, thread-safe bir koleksiyondur ve
    // özel bir sıralama garantisi sunmaz (no ordering).
    ConcurrentBag<Int32> bag = [];
    List<Task> tasks = [];

    for(Int32 i = 0; i < 10; i++) {
        Int32 i1 = i;
        tasks.Add(Task.Factory.StartNew(() => {
            // Her task, ConcurrentBag'a bir eleman ekliyor.
            bag.Add(i1);
            Console.WriteLine($"Task: {Task.CurrentId} has added {i1}");

            // Her task, ConcurrentBag'ın en üstündeki elemanı görmeye çalışıyor.
            // ConcurrentBag, LIFO veya FIFO gibi belirli bir sıralama garantisi vermez.
            if(bag.TryPeek(out Int32 result)) {
                Console.WriteLine($"Task: {Task.CurrentId} has peeked the value {result}");
            }
        }));
    }

    // Tüm task'lerin tamamlanmasını bekliyor.
    Task.WaitAll([.. tasks]);

    // ConcurrentBag'dan bir eleman çıkarmayı deniyor ve başarılı olursa yazdırıyor.
    // 'TryTake' metodu, eşzamanlılık durumunda güvenli bir şekilde eleman çıkarır.
    if(bag.TryTake(out Int32 last)) {
        Console.WriteLine($"I got {last}");
    }
}

void DemonstrateBlockingCollectionUsage() {
    /*
    BlockingCollection<T> .NET'te bulunan bir koleksiyon sınıfıdır ve çoklu thread ortamlarında güvenli bir şekilde veri paylaşımını sağlar. Bu koleksiyon, birden fazla üretici (producer) ve tüketici (consumer) arasında eşzamanlı erişimi yönetir ve aşağıdaki özelliklere sahiptir:

    1. Thread-Safe: Birden fazla thread'in aynı anda erişebileceği güvenli bir yapı sağlar.

    2. Bloklayıcı İşlemler: Koleksiyon boşken tüketici bekletilir, doluyken üretici bekletilir.

    3. Esnek Kapasite: Belirlenen bir sınır kapasitesi ile oluşturulabilir, sınır belirlenmezse varsayılan olarak çok büyük bir değer kullanılır.

    4. Çeşitli Temel Koleksiyonlarla Kullanım: Farklı türdeki temel koleksiyonlarla (örneğin ConcurrentQueue<T>, ConcurrentStack<T>) kullanılabilir, bu koleksiyonun davranışını değiştirir.

    5. Üretici-Tüketici Senaryoları: Üretici-tüketici deseni için idealdir, veri üretimi ve tüketimi arasında senkronizasyon sağlar.

    BlockingCollection<T>, paralel programlama ve çoklu iş parçacıklı uygulamalarda veri paylaşımı ve thread yönetimi için büyük kolaylık sağlar.
    */

    // BlockingCollection oluşturuluyor. Bu, ConcurrentBag kullanarak thread-safe bir koleksiyon sağlar.
    // Kapasitesi 10 olarak belirlenmiştir.
    BlockingCollection<Int32> messages = new(new ConcurrentBag<Int32>(), 10);

    // CancellationTokenSource ve CancellationToken oluşturuluyor.
    CancellationTokenSource cancellationTokenSource = new();
    CancellationToken token = cancellationTokenSource.Token;

    // Üretici ve tüketici işlemlerini başlatan task başlatılıyor.
    Task.Factory.StartNew(ProduceAndConsume, token);
    Console.ReadKey(); // Kullanıcıdan bir tuşa basması bekleniyor.
    cancellationTokenSource.Cancel(); // Tuşa basıldığında iptal işlemi tetikleniyor.

    void ProduceAndConsume() {
        // Üretici ve tüketici task'ları başlatılıyor.
        Task producer = Task.Factory.StartNew(RunProducer);
        Task consumer = Task.Factory.StartNew(RunConsumer);

        try {
            // Üretici ve tüketici task'larının tamamlanmasını bekliyor.
            Task.WaitAll([producer, consumer], token);
        }
        catch(AggregateException aggregateException) {
            // Hata durumunda hataları ele alıyor.
            aggregateException.Handle(exception => true);
        }
    }

    void RunConsumer() {
        // Tüketici işlemi. BlockingCollection'dan elemanları alıp işliyor.
        foreach(Int32 item in messages.GetConsumingEnumerable()) {
            token.ThrowIfCancellationRequested();
            Console.WriteLine($"Consumed item: {item}"); // Tüketilen elemanı yazdırıyor.
            Thread.Sleep(TimeSpan.FromMilliseconds(Random.Shared.Next(1000))); // Rastgele bir süre bekliyor.
        }
    }

    void RunProducer() {
        // Üretici işlemi. BlockingCollection'a eleman ekliyor.
        while(true) {
            token.ThrowIfCancellationRequested();
            Int32 item = Random.Shared.Next(100);
            messages.Add(item);
            Console.WriteLine($"Produced item: {item}"); // Üretilen elemanı yazdırıyor.
            Thread.Sleep(TimeSpan.FromMilliseconds(Random.Shared.Next(100))); // Rastgele bir süre bekliyor.
        }
    }
}