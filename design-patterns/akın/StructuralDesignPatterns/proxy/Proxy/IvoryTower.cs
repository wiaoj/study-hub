namespace Proxy;
public class IvoryTower : IWizardTower {
    public void Enter(Wizard wizard) {
        Logger.Information("{0} enters the tower.", wizard);
    }
}