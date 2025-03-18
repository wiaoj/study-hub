namespace State;
public class Mammoth {
    private IState state;

    public Mammoth() {
        this.state = new PeacefulState(this);
    }

    public void TimePasses() {
        if(this.state is PeacefulState) {
            ChangeStateTo(new AngryState(this));
        }
        else {
            ChangeStateTo(new PeacefulState(this));
        }
    }

    private void ChangeStateTo(IState newState) {
        this.state = newState;
        this.state.OnEnterState();
    }

    public sealed override String ToString() {
        return "The mammoth";
    }

    public void Observe() {
        this.state.Observe();
    }
}