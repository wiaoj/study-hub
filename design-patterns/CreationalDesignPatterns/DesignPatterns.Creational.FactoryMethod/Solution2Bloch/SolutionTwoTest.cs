namespace DesignPatterns.Creational.FactoryMethod.Solution2Bloch;
internal static class SolutionTwoTest {
    public static void Run() {
        Employee newEmployee = Employee.CreateNewEmployee(4, "Ali", "Sales");
        Employee newTempEmployee = Employee.CreateNewTemporaryEmployee(4, "Ali");
    }
}