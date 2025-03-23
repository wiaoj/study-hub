var msSqlDatabase = Database.GetInstance("MsSql");
msSqlDatabase
    .ConnectionString("Server=.;Database=Northwind;Integrated Security=True;");
msSqlDatabase.Connection();
var oracle = Database.GetInstance("Oracle");
var mongoDb = Database.GetInstance("MongoDb");

var msSqlDatabase2 = Database.GetInstance("MsSql");
var oracle2 = Database.GetInstance("Oracle");
var mongoDb2 = Database.GetInstance("MongoDb");

class Database {
    private static readonly Dictionary<String, Database> instances = [];

    private Database() {
        Console.WriteLine($"{nameof(Database)} nesnesi üretildi.");
    }

    public static Database GetInstance(String key) {
        if(!instances.ContainsKey(key)) {
            instances[key] = new Database();
        }
        return instances[key];
    }

    public void Connection() {
        Console.WriteLine($"{nameof(Database)} bağlantısı sağlandı.");
    }

    public void Disconnect() {
        Console.WriteLine($"{nameof(Database)} bağlantısı kesildi.");
    }

    public void ConnectionString(String connectionString) {
        Console.WriteLine($"{nameof(Database)} bağlantı cümlesi: {connectionString}");
    }
}