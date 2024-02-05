//// Sabit bir değer tanımlanıyor. Bu, işlenecek eleman sayısını belirtir.
//const Int32 count = 50;

//// 1'den başlayarak 'count' kadar eleman içeren bir dizi oluşturuluyor.
//Int32[] items = Enumerable.Range(1, count).ToArray();

//// Sonuçların saklanacağı, 'count' büyüklüğünde bir dizi oluşturuluyor.
//Int32[] results = new Int32[count];

//// 'items' dizisi üzerinde paralel bir döngü başlatılıyor.
//items.AsParallel().ForAll(x => {
//    // Her bir 'x' elemanının küpü hesaplanıyor.
//    Int32 newValue = x * x * x;
//    // Hesaplanan küp değeri ve o anki Task ID'si yazdırılıyor.
//    Console.Write($"{newValue} ({Task.CurrentId})\t");
//    // Hesaplanan küp değeri, 'results' dizisine atanıyor.
//    results[x - 1] = newValue;
//});
//// İki satır boşluk bırakılıyor.
//Console.WriteLine();
//Console.WriteLine();

//// 'results' dizisindeki her bir elemanı yazdırmak için kullanılan döngü.
//// Bu döngü şu anda yorum satırı olarak bırakılmıştır.
////foreach(var i in results) 
////    Console.WriteLine($"{i}");

//// 'items' dizisinin elemanları üzerinde paralel ve sıralı bir şekilde işlem yapılıyor.
//var cubes = items.AsParallel().AsOrdered().Select(x => x * x * x);

//// Hesaplanan küp değerleri sırayla yazdırılıyor.
//foreach(var item in cubes)
//    Console.Write($"{item}\t");



////Cancellation & Exceptions


//// CancellationTokenSource ve CancellationToken nesneleri oluşturuluyor.
//// Bu nesneler, paralel işlemleri iptal etmek için kullanılacak.
//CancellationTokenSource cancellationTokenSource = new();
//CancellationToken token = cancellationTokenSource.Token;

//// 1'den 20'ye kadar olan sayılar üzerinde paralel işlemler başlatılıyor.
//var items = ParallelEnumerable.Range(1, 20);

//// WithCancellation ile işlemlere iptal tokeni ekleniyor.
//// Bu sayede, belirli bir koşulda işlemleri iptal edebiliriz.
//var results = items.WithCancellation(token).Select(x => {
//    double result = Math.Log10(x); // Her bir x için 10 tabanında logaritma hesaplanıyor.

//    // Eğer sonuç 1'den büyükse bir hata fırlatılacak. (Bu satır şu an için yorum satırı olarak bırakılmıştır.)
//    // if(result > 1)
//    //     throw new InvalidOperationException();

//    // İşlem yapılan x değeri ve o anki Task ID'si yazdırılıyor.
//    Console.WriteLine($"x = {x}, TaskId = {Task.CurrentId}");
//    return result;
//});

//try {
//    // Hesaplanan sonuçlar üzerinde döngü başlatılıyor.
//    foreach(var item in results) {
//        // Eğer logaritma sonucu 1'den büyükse, işlem iptal ediliyor.
//        if(item > 1) {
//            cancellationTokenSource.Cancel();
//        }

//        // Hesaplanan logaritma sonucu yazdırılıyor.
//        Console.WriteLine($"Result = {item}");
//    }
//}
//catch(AggregateException aggregateException) {
//    // Paralel işlemler sırasında oluşan hatalar yakalanıyor.
//    aggregateException.Handle(exception => {
//        // Hata tipi ve mesajı yazdırılıyor.
//        Console.WriteLine($"{exception.GetType().Name}: {exception.Message}");
//        return true; // Hatanın ele alındığını belirtiyor.
//    });
//}
//catch(OperationCanceledException) {
//    // İşlemin iptal edildiği durumda bu blok çalışıyor.
//    Console.WriteLine("Operation was canceled");
//}


//// Merge Options

//Int32[] numbers = Enumerable.Range(1, 20).ToArray();

//// Paralel sorgu tanımlanıyor.
//ParallelQuery<Double> results = numbers.AsParallel()
//    .WithMergeOptions(ParallelMergeOptions.FullyBuffered) // Sonuçların nasıl birleştirileceğini belirleyen seçenek.
//    .Select(x => {
//        Double result = Math.Log10(x); // Her bir sayının 10 tabanında logaritması hesaplanıyor.
//        Console.Write($"P {result}\t"); // Paralel işlem sırasında hesaplanan sonuç ekrana yazdırılıyor.
//        return result;
//    });

///*
// NotBuffered

//Sonuçlar hesaplandıkça hemen döndürülür.
//Bu seçenek, sonuçları mümkün olan en kısa sürede almak istediğinizde kullanışlıdır.
//Ancak, sonuçların sırası korunmayabilir ve sonuçların işlenme sırası değişkenlik gösterebilir.
//AutoBuffered

//PLINQ, sonuçları bir miktar tamponlar, ancak tampon boyutu otomatik olarak yönetilir.
//Bu, bir denge sağlar; sonuçlar biraz gecikmeli döndürülür, ancak performans genellikle iyi bir seviyededir.
//Sonuçların döndürülme sırası genellikle daha öngörülebilir olur, ancak garantili değildir.
//FullyBuffered

//Tüm sonuçlar hesaplanıp tamponlandıktan sonra döndürülür.
//Bu seçenek, sonuçların sırasının önemli olduğu durumlar için uygundur çünkü tüm işlemler tamamlandıktan sonra sonuçlar sıralı bir şekilde döndürülür.
//Ancak, ilk sonucu almadan önce tüm işlemlerin tamamlanmasını beklemek gerektiğinden, gecikme en fazla olabilir.
//*/

//foreach(Double result in results) {
//    Console.Write($"C {result}\t");
//}


//Custom Aggregation

// Enumerable.Range ile 1'den 1000'e kadar olan sayıların toplamını hesaplar.
// Bu yöntem, belirtilen aralıktaki tüm sayıları tek tek toplar.
//var sum = Enumerable.Range(1, 1000).Sum();

// Enumerable.Range ile 1'den 1000'e kadar olan sayıları Aggregate fonksiyonu kullanarak toplar.
// İlk parametre olarak 0 (toplam için başlangıç değeri) alır.
// İkinci parametre, her bir iterasyonda toplamı ve mevcut sayıyı alıp yeni toplamı döndüren bir fonksiyondur.
// Bu yöntem, tüm sayıları sırayla işleyerek toplamı hesaplar.
//var sum = Enumerable.Range(1, 1000)
//    .Aggregate(0, (acc, i) => acc + i);

// ParallelEnumerable.Range ile 1'den 1000'e kadar olan sayıların paralel olarak toplamını hesaplar.
// 'seed': Her bir parça için başlangıç toplam değeri.
// 'updateAccumulatorFunc': Her bir parçadaki sayıları toplamak için kullanılan fonksiyon.
// 'combineAccumulatorsFunc': Paralel işlenen parçaların toplamlarını birleştiren fonksiyon.
// Son parametre, sonucu döndürmeden önce uygulanacak son işlevdir (bu örnekte, doğrudan sonucu döndürür).
var sum = ParallelEnumerable.Range(1, 1000)
    .Aggregate(seed: 0,
        updateAccumulatorFunc: (partialSum, i) => partialSum += i,
        combineAccumulatorsFunc: (total, subTotal) => total += subTotal,
        resultSelector: total => total);

// Yukarıdaki kod, çok çekirdekli işlemcilerde yüksek performans sağlamak için paralel işlemeyi kullanır.
// Bu, büyük veri setleri üzerinde çalışırken önemli zaman tasarrufu sağlayabilir.

Console.WriteLine(sum);