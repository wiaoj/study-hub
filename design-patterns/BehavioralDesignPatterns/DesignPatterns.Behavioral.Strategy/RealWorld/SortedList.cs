namespace DesignPatterns.Behavioral.Strategy.RealWorld;
public class SortedList {
    private List<String> list = [];
    private SortStrategy sortstrategy;
    public void SetSortStrategy(SortStrategy sortstrategy) {
        this.sortstrategy = sortstrategy;
    }

    public void Add(String name) {
        this.list.Add(name);
    }

    public void Sort() {
        this.sortstrategy.Sort(this.list);
        this.list.ForEach(name => Console.WriteLine($" {name}"));
        Console.WriteLine();
    }
}