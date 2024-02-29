using DesignPatterns.Behavioral.Strategy.RealWorld;

SortedList studentRecords = new();
studentRecords.Add("Samual");
studentRecords.Add("Jimmy");
studentRecords.Add("Sandra");
studentRecords.Add("Vivek");
studentRecords.Add("Anna");
studentRecords.SetSortStrategy(new ShellSort());
studentRecords.Sort();
studentRecords.SetSortStrategy(new MergeSort());
studentRecords.Sort();
studentRecords.SetSortStrategy(new QuickSort());
studentRecords.Sort();