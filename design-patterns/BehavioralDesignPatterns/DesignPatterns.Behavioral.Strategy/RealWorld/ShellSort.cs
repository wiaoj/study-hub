namespace DesignPatterns.Behavioral.Strategy.RealWorld;
public class ShellSort : SortStrategy {
    public override void Sort(List<String> list) {
        Console.WriteLine("ShellSorted list ");
    }
}