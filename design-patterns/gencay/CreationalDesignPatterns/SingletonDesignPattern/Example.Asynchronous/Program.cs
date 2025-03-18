
Task task1 = Task.Run(() => {
    Example2.GetInstance();
});

Task task2 = Task.Run(() => {
    Example2.GetInstance();
});

await Task.WhenAll(task1, task2);

class Example {
    private static Example? instance;
    private static readonly Lock @lock = new();
    private Example() {
        Console.WriteLine($"{nameof(Example)} nesnesi oluşturuldu.");
    }

    public static Example Instance {
        get {
            if(instance == null) {
                @lock.EnterScope();
                instance ??= new Example();
                @lock.Exit();
            }
            return instance;
        }
    }

    public static Example GetInstance() {
        return Instance;
    }
}

// Yukarıda ki kod yerine direkt static ctor kullanırsak direkt
// olarak lock işlemini yapmadan da 1 tane instance oluşturabiliriz.
class Example2 { 
    private Example2() {
        Console.WriteLine($"{nameof(Example)} nesnesi oluşturuldu.");
    }

    static Example2() {
        Instance = new Example2();
    }

    public static Example2 Instance { get; }

    public static Example2 GetInstance() {
        return Instance;
    }
}