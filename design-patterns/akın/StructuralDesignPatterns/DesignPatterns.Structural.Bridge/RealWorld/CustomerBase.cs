namespace DesignPatterns.Structural.Bridge.RealWorld;
public class CustomersBase {
    public required DataObject Data { set; get; }
    public virtual void Next() {
        this.Data.NextRecord();
    }
    public virtual void Prior() {
        this.Data.PriorRecord();
    }
    public virtual void Add(String customer) {
        this.Data.AddRecord(customer);
    }
    public virtual void Delete(String customer) {
        this.Data.DeleteRecord(customer);
    }
    public virtual void Show() {
        this.Data.ShowRecord();
    }
    public virtual void ShowAll() {
        this.Data.ShowAllRecords();
    }
}