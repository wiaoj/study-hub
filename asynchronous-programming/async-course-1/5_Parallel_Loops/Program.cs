//// Action delegeleri tanımlanıyor. Her biri Console.WriteLine ile bir string yazdırıyor.
//// Bu delegeler, paralel olarak çalıştırılacak işleri temsil eder.
//Action action1 = new(() => Console.WriteLine($"First {Task.CurrentId}"));
//Action action2 = new(() => Console.WriteLine($"Second {Task.CurrentId}"));
//Action action3 = new(() => Console.WriteLine($"Third {Task.CurrentId}"));

//// Parallel.Invoke, birden fazla Action'ı paralel olarak çalıştırır.
//// Bu, farklı iş parçacıklarında eş zamanlı olarak bu işlevleri çalıştırır.
//Parallel.Invoke(action1, action2, action3);

// Çıktı örneği (çıktı her çalıştırmada farklı olabilir):
// Third 10
// First 11
// Second 9
//*/

//// ParallelOptions, paralel işlemler için yapılandırma seçenekleri sağlar.
//var paralleOptions = new ParallelOptions {
//    MaxDegreeOfParallelism = 2 // Aynı anda çalıştırılacak maksimum iş parçacığı sayısını belirler.
//};

//// Parallel.For, belirli bir aralıktaki sayılar üzerinde paralel döngü yapar.
//// Bu örnekte, 1'den 10'a kadar olan sayıların kareleri hesaplanıyor.
//Parallel.For(1, 11,/*paralleOptions,*/ i => {
//    Console.WriteLine($"{i * i}");
//});

// Çıktı örneği (çıktı her çalıştırmada farklı olabilir):
// 16
// 1
// 36
// 64
// 9
// 49
// 81
// 100
// 4
// 25
//*/

//// String dizisi tanımlanıyor.
//String[] words = ["oh", "what", "a", "night"];

//// Parallel.ForEach, bir koleksiyon üzerinde paralel döngü yapar.
//// Bu örnekte, her kelimenin uzunluğu yazdırılıyor.
//Parallel.ForEach(words, word => {
//    Console.WriteLine($"'{word}' has length {word.Length} (task {Task.CurrentId})");
//});

// Çıktı örneği (çıktı her çalıştırmada farklı olabilir):
// oh has length 2 (task 4)
// a has length 1 (task 7)
// night has length 5 (task 5)
// what has length 4 (task 6) 
//*/

//// Range, belirli bir aralıkta sayıları üreten bir IEnumerable<Int32> döndüren bir metot.
//static IEnumerable<Int32> Range(Int32 start, Int32 end, Int32 step) {
//    for(Int32 i = start; i < end; i += step) {
//        yield return i;
//    }
//}

//// Parallel.ForEach ile Range fonksiyonundan dönen değerler üzerinde paralel döngü yapılıyor.
//Parallel.ForEach(Range(1, 20, 3), Console.WriteLine);

// Çıktı örneği (çıktı her çalıştırmada farklı olabilir):
// 13
// 1
// 7
// 4
// 16
// 10
// 19
//*/

//// Hata yönetimi için try-catch blokları kullanılıyor.
//try {
//    Demo();
//}
//catch(AggregateException exception) {
//    // AggregateException, paralel işlemler sırasında oluşan birden fazla hatayı yakalar.
//    // Handle metodu, her bir iç içe hata için belirtilen işlevi uygular.
//    exception.Handle(e => {
//        Console.WriteLine(e.Message); // Her bir hatanın mesajını yazdırır.
//        return true; // Hatanın başarıyla işlendiğini belirtir.
//    });
//}
//catch(OperationCanceledException exception) {
//    // İşlem iptal edildiğinde bu hata yakalanır.
//    Console.WriteLine(exception.Message);
//}

//void Demo() {
//    var cancellationTokenSource = new CancellationTokenSource();
//    var parallelOptions = new ParallelOptions {
//        CancellationToken = cancellationTokenSource.Token
//    };

//    var parallelLoopResult = Parallel.For(0, 20, parallelOptions, (int x, ParallelLoopState state) => {
//        Console.WriteLine($"{x}, [{Task.CurrentId}]");

//        if(x == 10) {
//            // İstisna fırlatma, döngü durdurma veya kırma seçenekleri:
//            // throw new Exception(); // Bir istisna fırlatır.
//            // state.Stop(); // Döngünün tamamlanmasını durdurur, ancak mevcut iterasyonlar tamamlanır.
//            // state.Break(); // Geçerli iterasyonun tamamlanmasını sağlar ve sonraki iterasyonları durdurur.
//            cancellationTokenSource.Cancel(); // İptal tokeni ile döngüyü iptal eder.
//        }
//    });

//    Console.WriteLine(new String('-', 5));
//    Console.WriteLine($"Was loop completed? {parallelLoopResult.IsCompleted}");
//    if(parallelLoopResult.LowestBreakIteration.HasValue) {
//        Console.WriteLine($"Lowest break iteration is {parallelLoopResult.LowestBreakIteration}");
//    }
//}

//// Toplamı saklamak için bir değişken tanımlanıyor.
//Int32 sum = 0;

//// Parallel.For metodu, paralel bir döngü başlatır.
//// 1'den 1001'e (1001 dahil değil) kadar olan sayılar üzerinde işlem yapılır.
//Parallel.For(
//    1, // Başlangıç değeri
//    1001, // Bitiş değeri (dahil değil)
//    () => 0, // localInit: Her bir iş parçacığı için başlangıçta çağrılır, yerel toplamı sıfırlar.
//    (x, state, localSum) => { // body: Her bir iterasyon için çağrılır.
//        localSum += x; // Yerel toplama, mevcut sayı eklenir.
//        Console.WriteLine($"Task {Task.CurrentId} has sum {localSum}");
//        return localSum; // Yerel toplamı döndürür.
//    },
//    partialSum => { // localFinally: Her bir iş parçacığının sonunda çağrılır.
//        Console.WriteLine($"Partial value of task {Task.CurrentId} is {partialSum}");
//        Interlocked.Add(ref sum, partialSum); // Yerel toplamları güvenli bir şekilde ana toplama ekler.
//    }
//);

//// Sonuç olarak, 1'den 1000'e kadar olan sayıların toplamı yazdırılır.
//Console.WriteLine($"Sum of 1..1000 = {sum}");


using System.Collections.Concurrent;

void SquareEachValue() {
    const Int32 count = 100_000; // İşlenecek eleman sayısı.

    // 0'dan başlayarak 'count' sayısına kadar olan tamsayıları içeren bir dizi oluşturuluyor.
    IEnumerable<Int32> values = Enumerable.Range(0, count);

    // Sonuçları saklamak için bir dizi tanımlanıyor.
    Int32[] results = new Int32[count];

    // Parallel.ForEach ile her bir sayının karesi hesaplanıyor.
    Parallel.ForEach(values, x => {
        results[x] = Convert.ToInt32(Math.Pow(x, 2)); // 'x' değerinin karesi hesaplanıp 'results' dizisine atanıyor.
    });
}


void SquareEachValueChunked() {
    const Int32 count = 100_000; // İşlenecek eleman sayısı.

    // 0'dan başlayarak 'count' sayısına kadar olan tamsayıları içeren bir dizi oluşturuluyor.
    IEnumerable<Int32> values = Enumerable.Range(0, count);

    // Sonuçları saklamak için bir dizi tanımlanıyor.
    Int32[] results = new Int32[count];

    // Parçalara ayırma işlemi için bir partitioner oluşturuluyor.
    OrderablePartitioner<Tuple<Int32, Int32>> partitioner = Partitioner.Create(0, count, 10_000);

    // Parallel.ForEach ile her bir parça üzerinde işlem yapılıyor.
    Parallel.ForEach(partitioner, range => {
        for(Int32 i = range.Item1; i < range.Item2; i++) {
            results[i] = (Int32)Math.Pow(i, 2); // 'i' değerinin karesi hesaplanıp 'results' dizisine atanıyor.
        }
    });
}


//Summary summary = BenchmarkRunner.Run<Program>();
//Console.WriteLine(summary);
