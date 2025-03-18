namespace State;
public class AngryState : IState {
    private readonly Mammoth mammoth;

    public AngryState(Mammoth mammoth) {
        this.mammoth = mammoth;
    }

    public void Observe() {
        Logger.Information($"{this.mammoth} is in a rage!");
    }

    public void OnEnterState() {
        Logger.Information($"{this.mammoth} roars in anger!");
    }
}