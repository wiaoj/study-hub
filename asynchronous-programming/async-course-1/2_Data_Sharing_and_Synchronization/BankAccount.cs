namespace _2_Data_Sharing_and_Synchronization;
public class BankAccount {
    private Int64 balance;

    public Int64 Balance { get => balance; private set => balance = value; }

    public void Deposit(Int64 amount) {
        // +=
        // operation1: temp -> get_Balance() + amount
        // operation2: set_Balance(temp) 
        
        // Eğer bu işlem sırasında kilitleme (lock) kullanılmazsa, çoklu iş parçacıkları bu iki işlem arasında
        // kesintiye uğrayabilir ve veri tutarlılığını bozabilir.

        // lock kullanarak iş parçacıklarının bu metoda eş zamanlı erişimini engelleyebiliriz:
        // lock(@object) { Balance += amount }

        // Interlocked.Add(ref balance, amount) kullanarak atomik bir şekilde bakiyeyi güncelleyebiliriz.
        // Bu, iş parçacığı güvenli bir şekilde bakiyeyi artırır ve veri yarışı koşullarını önler.

        // Hafıza bariyeri örneği:
        // Thread.MemoryBarrier();
        // Bu, iş parçacığı hafızasının durumunu senkronize eder ve emin olur ki işlem 1 ve 2
        // (yukarıda belirtilen) doğru sırada gerçekleşir.
        balance += amount;
    }

    public void Withdraw(Int64 amount) {
        // Interlocked.Add(ref balance, -amount) kullanarak atomik bir şekilde bakiyeyi azaltabiliriz.
        // Bu, iş parçacığı güvenli bir şekilde bakiyeyi azaltır ve veri yarışı koşullarını önler.

        // Monitor.TryEnter(,) kullanarak belirli bir süre için kilitleme denemesi yapılabilir.
        // Bu, belirli bir zaman aşımı süresi sonrasında kilidin serbest bırakılmasını sağlar.

        balance -= amount;
    }


    public void Transfer(BankAccount where, Int64 amount) {
        // Bu metod, bir hesaptan diğerine para transferi yapar.
        // Ancak, bu sürüm iş parçacığı güvenli değildir çünkü 'balance' değişkeni üzerinde
        // eş zamanlı işlemler veri tutarlılığını bozabilir.
        // Örneğin, iki iş parçacığı aynı anda bu metodu çağırırsa, bakiye güncellemeleri
        // birbirine karışabilir veya yanlış hesaplanabilir.

        balance -= amount;
        where.balance += amount;
    }

}