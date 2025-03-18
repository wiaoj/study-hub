namespace DesignPatterns.Structural.Bridge.RealWorld;
public abstract class DataObject {
    public abstract void NextRecord();
    public abstract void PriorRecord();
    public abstract void AddRecord(String name);
    public abstract void DeleteRecord(String name);
    public abstract String GetCurrentRecord();
    public abstract void ShowRecord();
    public abstract void ShowAllRecords();
}