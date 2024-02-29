using DesignPatterns.Creational.BuilderPattern.Method1;
using DesignPatterns.Creational.BuilderPattern.Method2;
using System.Text;

StringBuilder stringBuilder = new();

stringBuilder.Append("wiaoj").Append(" ").Append("git");
stringBuilder.Append(" git");

String name = stringBuilder.ToString();


EndpointBuilder endpointBuilder = new("https://localhost");

endpointBuilder
    .Append("api")
    .Append("v1")
    .Append("user")
    .AppendParam("id", "5")
    .AppendParam("username", "wiaoj");

String url = endpointBuilder.Build();

Console.WriteLine(url);


//EmployeeBuilderM1 employeeBuilderM1 = new();

//EmployeeM1 employeeM1 = employeeBuilderM1
//    .SetFullName("wiaoj test")
//    .SetUserName("wiaojtest")
//    .SetEmail("wiaoj@test.com")
//    .BuildEmployee();

//Console.WriteLine($"M1 Employee FirstName: {employeeM1.FirstName}");


//IEmployeeBuilderM2 employeeBuilderM2 = new InternalEmployeeBuilder();

//employeeBuilderM2.SetEmail("wiaoj@test.com");
//employeeBuilderM2.SetFullName("wiaoj test");
//EmployeeM2 employeeM2 = employeeBuilderM2.BuildEmployee();

//Console.WriteLine($"M2 Employee Email: {employeeM2.Email}");

EmployeeM2 employee = GenerateEmployee("wiaoj test", "wiaoj@test.com", 0);
Console.WriteLine($"Employee Email: {employee.Email}");
;

//EmployeeM2 GenerateEmployee(IEmployeeBuilderM2 employeeBuilder) {
//    return new();
//}

EmployeeM2 GenerateEmployee(String fullName, String email, Int32 employeeType) {
    IEmployeeBuilderM2 employeeBuilder = employeeType is default(Int32) ? new InternalEmployeeBuilder() : new ExternalEmployeeBuilder();

    employeeBuilder.SetFullName(fullName);
    employeeBuilder.SetEmail(email);

    return employeeBuilder.BuildEmployee();
}