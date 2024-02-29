using DesingPatterns.Structural.Facade.RealWorld;

//Facade facade = new Facade();
//facade.MethodA();
//facade.MethodB();

Mortgage mortgage = new();
Customer customer = new("Ann McKinsey");
String eligible = mortgage.IsEligible(customer, 125000) ? "Approved" : "Rejected";
Console.WriteLine($"\n{customer.Name} has been {eligible}");
