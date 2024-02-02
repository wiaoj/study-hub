namespace DesignPatterns.FactoryMethod.Solution1;
public class Director : Manager {
    protected Decimal Bonus { get; }

    public Director(Int32 no,
                    String name,
                    Int32 year,
                    String workingDepartment,
                    String managingDepartment,
                    Decimal bonus) : base(no, name, year, workingDepartment, managingDepartment) {
        this.Bonus = bonus;
    }

    public override void Work() {
        Console.WriteLine("Director is working!");
        Manage();
    }

    public override void Manage() {
        Console.WriteLine("Director manages whole company!");
        MakeAStrategicPlan();
    }

    public void MakeAStrategicPlan() {
        Console.WriteLine("Director makes a strategic plan for the company!");
    }

    public override Decimal CalculateSalary() {
        return base.CalculateSalary() + this.Bonus;
    }
}