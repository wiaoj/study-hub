namespace Proxy;
public class WizardTowerProxy : IWizardTower {
    private const Int32 NUM_WIZARDS_ALLOWED = 3;
    private Int32 numWizards;
    private readonly IWizardTower tower;

    public WizardTowerProxy(IWizardTower tower) {
        this.tower = tower;
    }


    public void Enter(Wizard wizard) {
        if(!CanAddMoreWizards()) {
            Logger.Information("{0} is not allowed to enter!", wizard);
            return;
        }

        this.tower.Enter(wizard);
        this.numWizards++;
    }

    private Boolean CanAddMoreWizards() {
        return this.numWizards < NUM_WIZARDS_ALLOWED;
    }
}