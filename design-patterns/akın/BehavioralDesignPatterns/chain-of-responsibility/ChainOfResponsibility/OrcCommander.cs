namespace ChainOfResponsibility;
public class OrcCommander : IRequestHandler {
    public Int32 Priority => 2;
    public String Name => "Orc Commander";

    public Boolean CanHandleRequest(Request request) {
        return request.RequestType == RequestType.DefendCastle;
    }

    public void Handle(Request request) {
        request.MarkHandled();
    }
}