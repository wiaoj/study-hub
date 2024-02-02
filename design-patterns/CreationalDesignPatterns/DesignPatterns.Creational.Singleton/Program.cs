using DesignPatterns.Singleton;
using System.Reflection;

//Client client = new();
//List<Int64> threadeSafeLazy = [];
//List<Int64> threadeSafeLazy2 = [];
//List<Int64> doubleCheckedLocking = [];

//Int32 testCount = 501;
//Int32 threadCount = 500;
//Console.WriteLine("Calculating...");

//while (testCount-- > 0) {
//    Console.WriteLine($"Test: {testCount}");
//    threadeSafeLazy.Add(client.ThreadeSafeLazy(threadCount));
//    threadeSafeLazy2.Add(client.ThreadeSafeLazy2(threadCount));
//    doubleCheckedLocking.Add(client.DoubleCheckedLocking(threadCount));
//}

//void PrintAverage(String name, List<Int64> list) {
//    Console.WriteLine($"{name}: {list.Average()}");
//}

//PrintAverage(nameof(threadeSafeLazy), threadeSafeLazy);
//PrintAverage(nameof(threadeSafeLazy2), threadeSafeLazy2);
//PrintAverage(nameof(doubleCheckedLocking), doubleCheckedLocking);

//Reflection Problem
Type singletonType = typeof(ThreadSafeLazySingleton2);

//for(Int32 i = 1; i <= 100; i++) {
//    Object? singleton = Activator.CreateInstance(singletonType, true);

//    if(singleton is null)
//        continue;

//    dynamic singletonObject = singleton;
//    Console.WriteLine($"Instance Created -> {singletonObject.Name}");
//}

Object? singleton1 = Activator.CreateInstance(singletonType, true);
Object? singleton2 = Activator.CreateInstance(singletonType, true);
Console.WriteLine(singleton1.GetHashCode() == singleton2.GetHashCode()); //FALSE, because of reflection
//use enumeration -> enum