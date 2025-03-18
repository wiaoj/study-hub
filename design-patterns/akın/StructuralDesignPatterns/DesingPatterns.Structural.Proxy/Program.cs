//using DesingPatterns.Structural.Proxy.RealWorldMath;

//MathProxy proxy = new();
//Console.WriteLine($"4 + 2 = {proxy.Add(4, 2)}");
//Console.WriteLine($"4 - 2 = {proxy.Sub(4, 2)}");
//Console.WriteLine($"4 * 2 = {proxy.Mul(4, 2)}");
//Console.WriteLine($"4 / 2 = {proxy.Div(4, 2)}");


using DesingPatterns.Structural.Proxy.RealWorld;

Console.WriteLine("Client passing employee with Role Developer to folderproxy");
Employee developer = new("Anurag", "Anurag123", "Developer");
SharedFolderProxy folderProxy1 = new(developer);
folderProxy1.PerformRWOperations();
Console.WriteLine();
Console.WriteLine("Client passing employee with Role Manager to folderproxy");
Employee manager = new("Pranaya", "Pranaya123", "Manager");
SharedFolderProxy folderProxy2 = new(manager);
folderProxy2.PerformRWOperations();
Console.Read();
