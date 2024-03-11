namespace ChainOfResponsibility;
public class OrcKing {
    private readonly List<IRequestHandler> handlers = [];

    public OrcKing() {
        BuildChain();
    }

    private void BuildChain() {
        this.handlers.Add(new OrcCommander());
        this.handlers.Add(new OrcOfficer());
        this.handlers.Add(new OrcSoldier());
    }


    public void MakeRequest(Request request) {
        this.handlers
            .OrderBy(handler => handler.Priority)
            .FirstOrDefault(handler => handler.CanHandleRequest(request))?
            .Handle(request);
    }
}