using System.Text;

namespace DesignPatterns.Creational.FactoryMethod.Solution1;
public class Employee {
    public Int32 No { get; set; }
    public String Name { get; set; }
    public Int32 Year { get; set; }
    public String Department { get; set; }
    public Decimal Salary { get; set; }

    public static readonly Decimal BASE_SALARY = 500;

    public Employee(Int32 no, String name, Int32 year, String department) {
        this.No = no;
        this.Name = name;
        this.Year = year;
        this.Department = department;
    }

    public virtual void Work() {
        Console.WriteLine("Employee is working!");
    }

    public virtual Decimal CalculateSalary() {
        this.Salary = this.Year * BASE_SALARY;
        return this.Salary;
    }

    public override String ToString() {
        StringBuilder builder = new("\nEmployee Info\t");
        builder.Append($"No: {this.No:000000}\t");
        builder.Append($"Name:{this.Name}\t");
        builder.Append($"Year: {this.Year:0#}\t");
        builder.Append($"Department: {this.Department}\t");
        builder.Append($"Salary: {CalculateSalary()}");
        return builder.ToString();
    }
}