
Example.GetInstance();
StaticExample staticExample = StaticExample.Instance; 

// 1. Yöntem
class Example {
    private static Int32 count = 1;
    private static Example? instance;

    private Example() {
        Console.WriteLine($"{count++}. {nameof(Example)} nesnesi oluşturuldu.");
    }

    public static Example Instance => instance ??= new Example();

    public static Example GetInstance() {
        return Instance;
    }
}

// 2. Yöntem
class StaticExample {
    private static Int32 count = 1;

    private StaticExample() { 
        Console.WriteLine($"{count++}. {nameof(StaticExample)} nesnesi oluşturuldu.");
    }

    static StaticExample() {
        Instance = new StaticExample();
    }

    public static StaticExample Instance { get; private set; }
}