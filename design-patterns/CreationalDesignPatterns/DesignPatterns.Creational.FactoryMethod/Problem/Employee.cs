using System.Text;

namespace DesignPatterns.FactoryMethod.Problem;
public sealed class Employee {
    public Int32 No { get; set; }
    public String Name { get; set; }
    public Int32 Year { get; set; }
    public String Department { get; set; }
    public Decimal Salary { get; set; }

    public String Type { get; }
    public String DepartmentManaged { get; }
    public Decimal Bonus { get; }

    public static readonly Decimal BASE_SALARY = 500;
    public static readonly Decimal MANAGEMENT_PAYMENT = 3000;

    public Employee(Int32 no, String name, Int32 year, String department, String type) {
        this.No = no;
        this.Name = name;
        this.Year = year;
        this.Department = department;
        this.Type = type;
    }

    public Employee(Int32 no, String name, Int32 year, String department, String type, String departmentManaged) {
        this.No = no;
        this.Name = name;
        this.Year = year;
        this.Department = department;
        this.DepartmentManaged = departmentManaged;
        this.Type = type;
    }

    public Employee(Int32 no, String name, Int32 year, String department, String type, String departmentManaged, Decimal bonus) {
        this.No = no;
        this.Name = name;
        this.Year = year;
        this.Department = department;
        this.DepartmentManaged = departmentManaged;
        this.Type = type;
        this.Bonus = bonus;
    }

    public void Work() {
        if(this.Type.Equals("Director")) {
            Manage();
            MakeAStrategicPlan();
        }
        else if(this.Type.Equals("Manager")) {
            Manage();
        }
        else {
            Console.WriteLine("Employee is working!");
        }
    }

    public Decimal CalculateSalary() {
        switch(this.Type) {
            case "Employee":
            this.Salary = this.Year * BASE_SALARY;
            break;
            case "Manager":
            this.Salary = this.Year * BASE_SALARY + MANAGEMENT_PAYMENT;
            break;
            case "Director":
            this.Salary = this.Year * BASE_SALARY + MANAGEMENT_PAYMENT + this.Bonus;
            break;
        }
        return this.Salary;
    }
    public void Manage() {
        if(this.Type.Equals("Director"))
            Console.WriteLine("Director manages whole company!");
        else if(this.Type.Equals("Manager"))
            Console.WriteLine($"Manager manages department: {this.DepartmentManaged}");
        else
            Console.WriteLine("I am just a poor Employee and can only manage myself!");
    }

    public void MakeAStrategicPlan() {
        if(this.Type.Equals("Director"))
            Console.WriteLine("Director makes a strategic plan for the company!");
        else
            Console.WriteLine("I am not a Director and can't make any strategic plan!");
    }

    public override String ToString() {
        StringBuilder builder = new("\nEmployee Info");
        builder.Append($"Type: {this.Type}");

        if(this.Type.Equals("Director"))
            builder.Append("Director of the company!");
        builder.Append($"No: {this.No}");
        builder.Append($"Name:{this.Name}");
        builder.Append($"Year: {this.Year}");
        builder.Append($"Department: {this.Department}");
        builder.Append($"Salary: {CalculateSalary()}");

        if(this.Type.Equals("Manager"))
            builder.Append($"Manages: {this.DepartmentManaged}");
        return builder.ToString();
    }
}