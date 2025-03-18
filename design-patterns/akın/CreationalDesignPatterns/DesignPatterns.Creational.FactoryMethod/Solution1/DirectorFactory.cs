namespace DesignPatterns.Creational.FactoryMethod.Solution1;
public class DirectorFactory : IFactory {
    public Employee Create() {
        Director director = new(Random.Shared.CreateEmployeeId(),
                                Random.Shared.CreateEmployeeName(),
                                Random.Shared.CreateEmployeeYear(),
                                "Management",
                                "Management",
                                5000);
        return director;
    }
}