namespace State;
public interface IState {
    void OnEnterState();
    void Observe(); 
}