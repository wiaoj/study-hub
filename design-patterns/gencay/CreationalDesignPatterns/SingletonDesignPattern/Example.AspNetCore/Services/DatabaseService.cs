namespace Example.AspNetCore.Services;

public sealed class DatabaseService {
    private static DatabaseService? instance;
    public int Count { get; private set; }
    private DatabaseService() {
        Console.WriteLine($"{nameof(DatabaseService)} is created");
    }

    public static DatabaseService Instance => instance ??= new DatabaseService();

    public Boolean Connect() {
        Console.WriteLine("Connected to database");
        Count++;
        return true;
    }

    public Boolean Disconnect() {
        Console.WriteLine("Disconnected from database");
        return true;
    }
}
