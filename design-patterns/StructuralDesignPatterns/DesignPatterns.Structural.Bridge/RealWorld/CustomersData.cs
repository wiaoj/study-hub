namespace DesignPatterns.Structural.Bridge.RealWorld;
public class CustomersData : DataObject {
    private readonly List<String> customers = [];
    private Int32 current = 0;
    private readonly String city;

    public CustomersData(String city) {
        this.city = city;
        this.customers.Add("Jim Jones");
        this.customers.Add("Samual Jackson");
        this.customers.Add("Allen Good");
        this.customers.Add("Ann Stills");
        this.customers.Add("Lisa Giolani");
    }

    public override void NextRecord() {
        if(this.current <= this.customers.Count - 1)
            this.current++;
    }

    public override void PriorRecord() {
        if(this.current > 0)
            this.current--;
    }

    public override void AddRecord(String name) {
        this.customers.Add(name);
    }

    public override void DeleteRecord(String name) {
        this.customers.Remove(name);
    }

    public override String GetCurrentRecord() {
        return this.customers[this.current];
    }
    public override void ShowRecord() {
        Console.WriteLine(this.customers[this.current]);
    }

    public override void ShowAllRecords() {
        Console.WriteLine($"Customer City: {this.city}");
        this.customers.ForEach(customer => Console.WriteLine($" {customer}"));
    }
}