namespace DesignPatterns.Behavioral.Strategy.RealWorld;
public class QuickSort : SortStrategy {
    public override void Sort(List<String> list) {
        list.Sort();
        Console.WriteLine("QuickSorted list ");
    }
}