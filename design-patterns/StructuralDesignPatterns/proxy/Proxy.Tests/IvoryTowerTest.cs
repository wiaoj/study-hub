namespace Proxy.Tests;
public class IvoryTowerTest {
    [Fact]
    public void TestEnter() {
        List<Wizard> wizards = Constants.Wizards;

        IvoryTower tower = new();
        wizards.ForEach(w => tower.Enter(w)); 

        Assert.Contains("Gandalf enters the tower.", Logger.Logs);
        Assert.Contains("Dumbledore enters the tower.", Logger.Logs);
        Assert.Contains("Oz enters the tower.", Logger.Logs);
        Assert.Contains("Merlin enters the tower.", Logger.Logs);
    }
}