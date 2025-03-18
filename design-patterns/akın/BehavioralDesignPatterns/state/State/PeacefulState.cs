namespace State;
public class PeacefulState : IState {
    private readonly Mammoth mammoth;
    
    public PeacefulState(Mammoth mammoth) {
        this.mammoth = mammoth;
    }

    public void Observe() {
        Logger.Information($"{this.mammoth} is grazing peacefully.");
    }

    public void OnEnterState() {
        Logger.Information($"{this.mammoth} sighs contentedly.");
    }
}