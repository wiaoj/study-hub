// Observer Design Pattern - Behavioral Category \\

Product samsung = new("Samsung S23", 6000);
Product apple = new("Iphone 14", 6000);

Amazon amazon = new();

StockObserver stockObserver = new("Wiaoj");
StockObserver2 stockObserver2 = new("Wiaoj");

amazon.Register(stockObserver, samsung);
amazon.Register(stockObserver2, apple);

amazon.NotifyForProductName(samsung.Name); //Wiaoj, Product Samsung S23 in stock now!
amazon.NotifyForProductName("Samsung S22");
amazon.NotifyForProductName(apple.Name); // Wiaoj, Product Iphone 14 in stock now!
Console.WriteLine(new String('-', 50));
amazon.NotifyAll(); // hepsini notify eder

Console.ReadLine();

class Amazon { // subject
    // subject bir şey olduğunda observer'larına haber verip, bilgilendiriyor
    private Dictionary<IObserver, Product> observers = new();

    public void Register(IObserver observer, Product product) {
        this.observers.TryAdd(observer, product);

    }

    public void UnRegister(IObserver observer) {
        this.observers.Remove(observer);
    }

    public void NotifyAll() {
        foreach(KeyValuePair<IObserver, Product> observer in this.observers) {
            observer.Key.Notify(observer.Value);
        }
    }

    public void NotifyForProductName(String productName) {
        foreach(var observer in this.observers) {
            if(observer.Value.Name == productName) {
                observer.Key.Notify(observer.Value);
            }
        }
    }
}

interface IObserver {
    public void Notify(Product product);
}

class StockObserver : IObserver { // observer
    public String FullName { get; set; }

    public StockObserver(String fullName) {
        this.FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
    }

    public void Notify(Product product) {
        Console.WriteLine($"{this.FullName}, Product {product.Name} in stock now!");
    }
}

class StockObserver2 : IObserver {
    public String FullName { get; set; }

    public StockObserver2(String fullName) {
        this.FullName = fullName;
    }

    public void Notify(Product product) {
        Console.WriteLine($"{this.FullName}, Product {product.Name} in stock now!");
    }
}

class Product {
    public String Name { get; set; }
    public Decimal Price { get; set; }

    public Product(String name, Decimal price) {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.Price = price;
    }
}