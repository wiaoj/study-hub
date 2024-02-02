namespace DesignPatterns.FactoryMethod.Solution1;
public class Manager : Employee {
    protected String DepartmentManaged { get; }

    public static readonly Int32 MANAGEMENT_PAYMENT = 3000;

    public Manager(Int32 no,
                   String name,
                   Int32 year,
                   String workingDepartment,
                   String departmentManaged) : base(no, name, year, workingDepartment) {
        this.DepartmentManaged = departmentManaged;
    }

    public override void Work() {
        Console.WriteLine("Manager is working!");
        Manage();
    }

    public virtual void Manage() {
        Console.WriteLine($"Manager manages department: {this.DepartmentManaged}");
    }

    public override Decimal CalculateSalary() {
        return base.CalculateSalary() + MANAGEMENT_PAYMENT;
    }
}