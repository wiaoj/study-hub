using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Course.Protobuf.Test;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

Console.WriteLine("Welcome to Protobuf test!");

Employee employee = new() {
    Id = 1,
    FirstName = "John",
    LastName = "Snow",
    IsRetired = false
};

DateTime birthDate = new(2000, 1, 1);
birthDate = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);
employee.BirthDate = Timestamp.FromDateTime(birthDate);
//employee.Age = 12;
employee.MaritalStatus = Employee.Types.MaritalStatus.Married;
employee.PreviousEmployers.Add("Employer 1");
employee.PreviousEmployers.Add("Employer 2");
employee.CurrentAddress = new Address {
    City = "New York",
    StreetName = "Avenue",
    HouseNumber = 31
};
employee.Relatives.Add("father", "father");
employee.Relatives.Add("mother", "mother");
employee.Relatives.Add("brother", "brother");
employee.Relatives.Add("sister", "sister");

WriteToFile(employee);
Employee employeeFromFile = ReadFromFile();

String output = JsonSerializer.Serialize(employeeFromFile, new JsonSerializerOptions() {
    WriteIndented = true
});

Console.WriteLine(output);
Console.WriteLine("Protobuf test completed!");



const String DataFileName = "employee.data";
void WriteToFile(Employee employee) {
    using FileStream output = File.Create(DataFileName);
    employee.WriteTo(output);
}

Employee ReadFromFile() {
    using FileStream input = File.OpenRead(DataFileName);
    return Employee.Parser.ParseFrom(input);
}