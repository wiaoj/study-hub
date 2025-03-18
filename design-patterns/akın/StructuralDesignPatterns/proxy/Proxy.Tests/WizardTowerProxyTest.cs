namespace Proxy.Tests;
public class WizardTowerProxyTest {
    [Fact]
    public void TestEnter() {
        List<Wizard> wizards = Constants.Wizards;

        IvoryTower tower = new();
        WizardTowerProxy proxy = new(tower);

        wizards.ForEach(wizard => proxy.Enter(wizard));

        Assert.Contains($"Gandalf enters the tower.", Logger.Logs);
        Assert.Contains($"Dumbledore enters the tower.", Logger.Logs);
        Assert.Contains($"Oz enters the tower.", Logger.Logs);
        Assert.Contains($"Merlin is not allowed to enter!", Logger.Logs);
    }
}