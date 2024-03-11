namespace ChainOfResponsibility;
public class OrcOfficer : IRequestHandler {
    public Int32 Priority => 3;
    public String Name => "Orc Officer";

    public Boolean CanHandleRequest(Request request) {
        return request.RequestType == RequestType.TorturePrisoner;
    }

    public void Handle(Request request) {
        request.MarkHandled();
    }
}