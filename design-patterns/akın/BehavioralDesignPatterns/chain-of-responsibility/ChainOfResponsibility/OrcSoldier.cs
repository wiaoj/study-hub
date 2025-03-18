namespace ChainOfResponsibility;
public class OrcSoldier : IRequestHandler {
    public Int32 Priority => 1;
    public String Name => "Orc Soldier";

    public Boolean CanHandleRequest(Request request) {
        return request.RequestType == RequestType.CollectTax;
    }

    public void Handle(Request request) {
        request.MarkHandled();
    }
}